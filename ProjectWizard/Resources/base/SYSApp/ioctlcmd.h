//======================================================================
// 
// ioctlcmd.h
//
// Copyright (C) 2002 Mark Russinovich
//
// This file contains driver IOCTLs and definitions shared by the
// driver and the GUI.
//
//======================================================================

//#pragma warning( disable : 4100 ) // Unreferenced formal parameter.
//#pragma warning( disable : 4127 ) // Conditional expression is constant.

// Device type
#define FILE_DEVICE_MYFAULT     0x00008336

// IOCTLS
#define IOCTL_WILD_POINTER     (ULONG) CTL_CODE( FILE_DEVICE_MYFAULT, 0x01, METHOD_BUFFERED, FILE_ANY_ACCESS )
#define IOCTL_TERM_PROCESSES   (ULONG) CTL_CODE( FILE_DEVICE_MYFAULT, 0x15, METHOD_BUFFERED, FILE_ANY_ACCESS )