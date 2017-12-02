using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.IO;


namespace EPocalipse.IFilter
{
  [ComVisible(false)]
  [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00000001-0000-0000-C000-000000000046")]
  internal interface IClassFactory
  {
    void CreateInstance([MarshalAs(UnmanagedType.Interface)] object pUnkOuter, ref Guid refiid, [MarshalAs(UnmanagedType.Interface)] out object ppunk);
    void LockServer(bool fLock);
  }


  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
 Guid("0000010c-0000-0000-C000-000000000046")]

  public interface IPersist
  {
      void GetClassID( /* [out] */ out Guid pClassID);
  };

  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
  Guid("00000109-0000-0000-C000-000000000046")]
  public interface IPersistStream : IPersist
  {
      new void GetClassID(out Guid pClassID);

      [PreserveSig]
      int IsDirty();
      void Load([In] IStream pStm);
      void Save([In] IStream pStm, [In, MarshalAs(UnmanagedType.Bool)] bool fClearDirty);
      void GetSizeMax(out long pcbSize);
  };


  /// <summary>
  /// Utility class to get a Class Factory for a certain Class ID 
  /// by loading the dll that implements that class
  /// </summary>
  internal static class ComHelper
  {
    //DllGetClassObject fuction pointer signature
    private delegate int DllGetClassObject(ref Guid ClassId, ref Guid InterfaceId, [Out, MarshalAs(UnmanagedType.Interface)] out object ppunk);

    //Some win32 methods to load\unload dlls and get a function pointer
    private class Win32NativeMethods
    {
      [DllImport("kernel32.dll", CharSet=CharSet.Ansi)]
      public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

      [DllImport("kernel32.dll")]
      public static extern bool FreeLibrary(IntPtr hModule);

      [DllImport("kernel32.dll")]
      public static extern IntPtr LoadLibrary(string lpFileName);
    }

    /// <summary>
    /// Holds a list of dll handles and unloads the dlls 
    /// in the destructor
    /// </summary>
    private class DllList
    {
      private List<IntPtr> _dllList=new List<IntPtr>();
      public void AddDllHandle(IntPtr dllHandle)
      {
        lock (_dllList)
        {
          _dllList.Add(dllHandle);
        }
      }

      ~DllList()
      {
        foreach (IntPtr dllHandle in _dllList)
        {
          try
          {
            Win32NativeMethods.FreeLibrary(dllHandle);
          }
          catch { };
        }
      }
    }

    static DllList _dllList=new DllList();

    /// <summary>
    /// Gets a class factory for a specific COM Class ID. 
    /// </summary>
    /// <param name="dllName">The dll where the COM class is implemented</param>
    /// <param name="filterPersistClass">The requested Class ID</param>
    /// <returns>IClassFactory instance used to create instances of that class</returns>
    internal static IClassFactory GetClassFactory(string dllName, string filterPersistClass)
    {
      //Load the class factory from the dll
      IClassFactory classFactory=GetClassFactoryFromDll(dllName, filterPersistClass);
      return classFactory;
    }

    private static IClassFactory GetClassFactoryFromDll(string dllName, string filterPersistClass)
    {
      //Load the dll
      IntPtr dllHandle=Win32NativeMethods.LoadLibrary(dllName);
      if (dllHandle==IntPtr.Zero)
        return null;

      //Keep a reference to the dll until the process\AppDomain dies
      _dllList.AddDllHandle(dllHandle);

      //Get a pointer to the DllGetClassObject function
      IntPtr dllGetClassObjectPtr=Win32NativeMethods.GetProcAddress(dllHandle, "DllGetClassObject");
      if (dllGetClassObjectPtr==IntPtr.Zero)
        return null;

      //Convert the function pointer to a .net delegate
      DllGetClassObject dllGetClassObject=(DllGetClassObject)Marshal.GetDelegateForFunctionPointer(dllGetClassObjectPtr, typeof(DllGetClassObject));

      //Call the DllGetClassObject to retreive a class factory for out Filter class
      Guid filterPersistGUID=new Guid(filterPersistClass);
      Guid IClassFactoryGUID=new Guid("00000001-0000-0000-C000-000000000046"); //IClassFactory class id
      Object unk;
      if (dllGetClassObject(ref filterPersistGUID, ref IClassFactoryGUID, out unk)!=0)
        return null;

      //Yippie! cast the returned object to IClassFactory
      return (unk as IClassFactory);
    }

  }

  public class StreamWrapper : IStream
  {
      public StreamWrapper(Stream stream)
      {
          if (stream == null)
              throw new ArgumentNullException("stream", "Can't wrap null stream.");
          this.stream = stream;
      }

      private Stream stream;

      public void Read(byte[] pv, int cb, System.IntPtr pcbRead)
      {
          Marshal.WriteInt32(pcbRead, (Int32)stream.Read(pv, 0, cb));
      }

      public void Write(byte[] pv, int cb, IntPtr pcbWritten)
      {
          int written = Marshal.ReadInt32(pcbWritten);
          stream.Write(pv, 0, written);
      }

      public void Seek(long dlibMove, int dwOrigin, System.IntPtr plibNewPosition)
      {
          //Marshal.WriteInt32(plibNewPosition, (int)stream.Seek(dlibMove, (SeekOrigin)dwOrigin));
          stream.Seek(dlibMove, (SeekOrigin)(dwOrigin));
      }

      public void Clone(out IStream ppstm)
      {
          throw new NotImplementedException();
      }

      public void Commit(int grfCommitFlags)
      {
          throw new NotImplementedException();
      }

      public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
      {
          throw new NotImplementedException();
      }

      public void LockRegion(long libOffset, long cb, int dwLockType)
      {
          throw new NotImplementedException();
      }

      public void Revert()
      {
          throw new NotImplementedException();
      }

      public void SetSize(long libNewSize)
      {
          throw new NotImplementedException();
      }

      public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
      {
          //IStreamWrapper wants the length
          var tempSTATSTG = new System.Runtime.InteropServices.ComTypes.STATSTG();
          tempSTATSTG.cbSize = stream.Length;
          pstatstg = tempSTATSTG;
      }

      public void UnlockRegion(long libOffset, long cb, int dwLockType)
      {
          throw new NotImplementedException();
      }
  }

  public class IStreamWrapper : Stream
  {
      IStream stream;

      public IStreamWrapper(IStream stream)
      {
          if (stream == null)
              throw new ArgumentNullException("stream");
          this.stream = stream;
      }

      ~IStreamWrapper()
      {
          Close();
      }

      public override int Read(byte[] buffer, int offset, int count)
      {
          if (offset != 0)
              throw new NotSupportedException("only 0 offset is supported");
          if (buffer.Length < count)
              throw new NotSupportedException("buffer is not large enough");

          IntPtr bytesRead = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)));
          try
          {
              stream.Read(buffer, count, bytesRead);
              return Marshal.ReadInt32(bytesRead);
          }
          finally
          {
              Marshal.FreeCoTaskMem(bytesRead);
          }
      }


      public override void Write(byte[] buffer, int offset, int count)
      {
          if (offset != 0)
              throw new NotSupportedException("only 0 offset is supported");
          stream.Write(buffer, count, IntPtr.Zero);
      }

      public override long Seek(long offset, SeekOrigin origin)
      {
          IntPtr address = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)));
          try
          {
              stream.Seek(offset, (int)origin, address);
              return Marshal.ReadInt32(address);
          }
          finally
          {
              Marshal.FreeCoTaskMem(address);
          }
      }


      public override long Length
      {
          get
          {
              System.Runtime.InteropServices.ComTypes.STATSTG statstg;
              stream.Stat(out statstg, 1 /* STATSFLAG_NONAME*/ );
              return statstg.cbSize;
          }
      }

      public override long Position
      {
          get { return Seek(0, SeekOrigin.Current); }
          set { Seek(value, SeekOrigin.Begin); }
      }


      public override void SetLength(long value)
      {
          stream.SetSize(value);
      }

      public override void Close()
      {
          stream.Commit(0);
          // Marshal.ReleaseComObject(stream);
          stream = null;
          GC.SuppressFinalize(this);
      }

      public override void Flush()
      {
          stream.Commit(0);
      }

      public override bool CanRead
      {
          get { return true; }
      }

      public override bool CanWrite
      {
          get { return true; }
      }

      public override bool CanSeek
      {
          get { return true; }
      }
  }

}
