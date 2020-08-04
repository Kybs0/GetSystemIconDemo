using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GetSystemIconDemo
{
    public static class SystemIconHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;

        }
        /// <summary>
        /// 返回系统设置的图标
        /// </summary>
        /// <param name="pszPath">文件路径 如果为""  返回文件夹的</param>
        /// <param name="dwFileAttributes">0</param>
        /// <param name="psfi">结构体</param>
        /// <param name="cbSizeFileInfo">结构体大小</param>
        /// <param name="uFlags">枚举类型</param>
        /// <returns>-1失败</returns>
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public enum SHGFI
        {
            SHGFI_ICON = 0x100,
            SHGFI_LARGEICON = 0x0,
            SHGFI_USEFILEATTRIBUTES = 0x10
        }

        /// <summary>
        /// 获取文件图标
        /// </summary>
        /// <param name="filePath">文件全路径</param>
        /// <returns>图标</returns>
        public static Icon GetFileIcon(string filePath)
        {
            SHFILEINFO shfileinfo = new SHFILEINFO();
            IntPtr iconIntPtr = SHGetFileInfo(filePath, 0, ref shfileinfo, (uint)Marshal.SizeOf(shfileinfo), (uint)(SHGFI.SHGFI_USEFILEATTRIBUTES| SHGFI.SHGFI_LARGEICON |SHGFI.SHGFI_ICON ));
            if (iconIntPtr.Equals(IntPtr.Zero))
                return null;
            Icon fileIcon = System.Drawing.Icon.FromHandle(shfileinfo.hIcon);
            return fileIcon;
        }

        /// <summary>
        /// 获取文件夹图标
        /// </summary>
        /// <returns>图标</returns>
        public static Icon GetDirectoryIcon()
        {
            SHFILEINFO shfileinfo = new SHFILEINFO();
            IntPtr fileInfo = SHGetFileInfo(@"", 0, ref shfileinfo, (uint)Marshal.SizeOf(shfileinfo), (uint)( SHGFI.SHGFI_LARGEICON|SHGFI.SHGFI_ICON));
            if (fileInfo.Equals(IntPtr.Zero))
                return null;
            Icon directoryIcon = System.Drawing.Icon.FromHandle(shfileinfo.hIcon);
            return directoryIcon;
        }
    }
}
