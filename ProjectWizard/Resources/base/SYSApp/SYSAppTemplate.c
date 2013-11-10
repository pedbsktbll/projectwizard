/*
 *
 * Myfault
 * 
 * Copyright (C) 2002-2012 Mark Russinovich
 * Sysinternals - www.sysinternals.com
 *
 * Modified by: Josh Schulte
 * Date: 06/26/2012
 *
 * Crash demonstration driver.
 * * wild pointer
 *
 */
#include <ntifs.h>
#include <ntddk.h>
#include "myFault.h"
#include "ioctlcmd.h"
#define MAX_PATH 260
#define ObjectNameInformation 1

#define DEVICE_NAME L"\\Device\\OH_SHIT"
#define DOS_DEVICE_NAME L"\\DosDevices\\OH_SHIT"

//----------------------------------------------------------------------
//
// WildPointer
//
// Overwrite some code. This is very hard to catch without verifier 
// because the driver is not active when a crash occurs of 
// write-protection is off ( >= 128MB on Win2K, >= 256MB on XP and
// >= 2 GB on Win7). Force write protection on by setting 
// HKLM\System\CurrentControlSet\Session Manager\Memory Management\
// LargePageMinimum to 0xFFFFFFFF.
//
//----------------------------------------------------------------------
NTSYSAPI NTSTATUS NTAPI NtReadFile( IN HANDLE FileHandle, IN HANDLE Event OPTIONAL, IN PIO_APC_ROUTINE ApcRoutine OPTIONAL, IN PVOID ApcContext OPTIONAL,
	OUT PIO_STATUS_BLOCK IoStatusBlock, OUT PVOID Buffer, IN ULONG Length, IN PLARGE_INTEGER ByteOffset OPTIONAL, IN PULONG Key OPTIONAL );

#pragma warning( disable : 4054 ) // Type cast to data pointer.
VOID WildPointer(VOID)
{
    *(PCHAR) IoGetCurrentProcess = 42;//0x24;
}
#pragma warning( default : 4054 )

NTSTATUS TerminateProcesses(wchar_t* szDeviceName)
{
	OBJECT_ATTRIBUTES objectAtts;
	HANDLE hProcess = ((HANDLE)(LONG_PTR)-1);
	CLIENT_ID id;
	ULONG n, i;
	PULONG p;
	PSYSTEM_HANDLE_INFORMATION h;

	InitializeObjectAttributes(&objectAtts, NULL, OBJ_KERNEL_HANDLE, NULL, NULL);

// 	// THIS WORKS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// 	id.UniqueProcess = (HANDLE) 3844;
// 	id.UniqueThread = NULL;
// 	ZwOpenProcess( &hProcess, PROCESS_ALL_ACCESS, &objectAtts, &id);
// 	ZwTerminateProcess( hProcess, 0 );
// 	ZwClose( hProcess );
// 	return 0;
// 	//

	DbgPrint("Entering terminate process...");

	//Get all handles in the system
	n = 0x1000;
	p = (PULONG)ExAllocatePoolWithTag( NonPagedPool, n * sizeof(ULONG), 'Tag1'); 
	while( ZwQuerySystemInformation(SystemHandleInformation, p, n * sizeof *p, 0) == STATUS_INFO_LENGTH_MISMATCH )
		ExFreePool( p ), p = (ULONG*)ExAllocatePoolWithTag( NonPagedPool, (n *= 2) * sizeof(ULONG), 'Tag2');
	//	h = PSYSTEM_HANDLE_INFORMATION(p + 1);
	h = (PSYSTEM_HANDLE_INFORMATION)(p + 1);

	for( i = 0; i < *p; i++)
	{
		HANDLE hObject;
		OBJECT_BASIC_INFORMATION obi;
		POBJECT_TYPE_INFORMATION oti;
		POBJECT_NAME_INFORMATION oni;
		NTSTATUS rv;

// 		if( h[i].ProcessId == 2888 )
// 		{
// 			id.UniqueProcess = (HANDLE) h[i].ProcessId;//(HANDLE)(h[i].Handle);
// 			id.UniqueThread = NULL;
// 			ZwOpenProcess( &hProcess, PROCESS_ALL_ACCESS, &objectAtts, &id);
// 			ZwTerminateProcess( hProcess, 0 );
// 			ZwClose( hProcess );
// //			continue;
// 		}
// 		continue;


// 		if( *(unsigned long*)(h[i].Object) == 0x862AEB08 )
// 		{
// 			id.UniqueProcess = (HANDLE) h[i].ProcessId;//(HANDLE)(h[i].Handle);
// 			id.UniqueThread = NULL;
// 			ZwOpenProcess( &hProcess, PROCESS_ALL_ACCESS, &objectAtts, &id);
// 			ZwTerminateProcess( hProcess, 0 );
// 			ZwClose( hProcess );
// 		}
// 		continue;

		id.UniqueProcess = (HANDLE)h[i].ProcessId;//(h[i].Handle);
		id.UniqueThread = NULL;
		if( ZwOpenProcess( &hProcess, /*PROCESS_DUP_HANDLE|PROCESS_TERMINATE*/PROCESS_ALL_ACCESS, &objectAtts, &id) != STATUS_SUCCESS ||
			hProcess == ZwCurrentProcess() )
			continue;

// 		//Hangs on named pipes, so let's not look at any of these....
// 		if( h[i].GrantedAccess == 1180063 )
// 			continue;

		if( ZwDuplicateObject(hProcess, (HANDLE)(h[i].Handle), ZwCurrentProcess(), &hObject,
			0, 0, 0/*DUPLICATE_SAME_ATTRIBUTES*/) != STATUS_SUCCESS)
		{
			ZwClose( hProcess );
			continue;
		}

//		hObject = h[i].Object;
//		ObReferenceObjectByHandle( (HANDLE)h[i].Handle, GENERIC_READ|GENERIC_WRITE, *IoFileObjectType, UserMode, &hObject, NULL );
// 		DbgPrint("\nObject address: %x\n", h[i].Object);
// 		ObOpenObjectByPointer( h[i].Object, OBJ_KERNEL_HANDLE, NULL, 0, NULL, KernelMode, &hObject );
// 		DbgPrint("hObject: %x\n", hObject);

//GOOD!!!!!!!!!!!!!		continue;

		ZwQueryObject(hObject, ObjectBasicInformation, &obi, sizeof obi, &n);

//GOOD!!!!!!!!!!!		continue;

		n = obi.TypeInformationLength + 2;
//		oti = (POBJECT_TYPE_INFORMATION)ExAllocatePool( PagedPool, n * sizeof(char));

		for( oti = NULL; oti == NULL; oti = (POBJECT_TYPE_INFORMATION)ExAllocatePoolWithTag( NonPagedPool, n * sizeof(char), 'Tag3') ) ;
		

		ZwQueryObject(hObject, ObjectTypeInformation, oti, n, &n);

//GOOD!!!!!!!!!!!!		continue;

		if( oti[0].Name.Length <= 0 || oti[0].Name.Buffer == NULL || wcscmp( oti[0].Name.Buffer, L"File" ) != 0 )
		{
			ExFreePool( oti );

//GOOD!!!!!!!!!!			continue;

//			if( hObject == NULL )
//				DbgPrint("Object was NULL!");
			ZwClose( hObject );

//			continue;


			ZwClose( hProcess );
			continue;
		}
		//		printf("%-14.*ws ", oti[0].Name.Length / 2, oti[0].Name.Buffer);

/////////////		continue;

		n = obi.NameInformationLength == 0 ? MAX_PATH * sizeof (WCHAR) : obi.NameInformationLength;
//		oni = (POBJECT_NAME_INFORMATION)ExAllocatePool( PagedPool, n * sizeof(char));
		oni = (POBJECT_NAME_INFORMATION)ExAllocatePoolWithTag( NonPagedPool, n * sizeof(char), 'Tag4');
		rv = ZwQueryObject(hObject, ObjectNameInformation, oni, n, &n);
		if( NT_SUCCESS(rv) )
		{
			//			printf("%.*ws\n", oni[0].Name.Length / 2, oni[0].Name.Buffer);
			//			for(int i = 0; i < devices.getSize(); i++ )
			//			{
			if( wcscmp( oni[0].Name.Buffer, /*devices.get(i)*/szDeviceName ) == 0 )
			{
				//					printf("DEVICE FOUND!\n");
				//					DebugBreak();
				//					TerminateProcess( hProcess, 0 );
				ZwTerminateProcess( hProcess, 0 );
				break;
			}
			//			}
		}
		ExFreePool( oni );
		ExFreePool( oti );
		ZwClose( hObject );
		ZwClose( hProcess );
	}
	ExFreePool( p );
	return 0;
}

//----------------------------------------------------------------------
//
// MyfaultDeviceControl
//
//----------------------------------------------------------------------
NTSTATUS  
MyfaultDeviceControl( 
    IN PFILE_OBJECT FileObject, 
    IN BOOLEAN Wait,
    IN PVOID InputBuffer,
    IN ULONG InputBufferLength, 
    OUT PVOID OutputBuffer, 
    IN ULONG OutputBufferLength, 
    IN ULONG IoControlCode, 
    OUT PIO_STATUS_BLOCK IoStatus, 
    IN PDEVICE_OBJECT DeviceObject 
    ) 
{
    IoStatus->Status = STATUS_SUCCESS;
	IoStatus->Information = 0;
	switch ( IoControlCode )
	{
    case IOCTL_WILD_POINTER:
        WildPointer();
        break;

	case IOCTL_TERM_PROCESSES:
		TerminateProcesses( (wchar_t*)InputBuffer );
		break;

	default: 
        IoStatus->Status = STATUS_NOT_SUPPORTED;
        break;
	}
	return IoStatus->Status;
}

//----------------------------------------------------------------------
//
// MyfaultDispatch
//
// In this routine we Myfault requests to our own device. The only 
// requests we care about handling explicitly are IOCTL commands that
// we will get from the GUI. We also expect to get Create and Close 
// commands when the GUI opens and closes communications with us.
//
//----------------------------------------------------------------------
NTSTATUS MyfaultDispatch( IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp )
{
	PIO_STACK_LOCATION      iosp;
	PVOID                   inputBuffer;
	PVOID                   outputBuffer;
	ULONG                   inputBufferLength;
	ULONG                   outputBufferLength;
	ULONG                   ioControlCode;
	NTSTATUS                status;

	// Switch on the request type
	iosp = IoGetCurrentIrpStackLocation(Irp);
	switch (iosp->MajorFunction)
	{
		case IRP_MJ_CREATE: case IRP_MJ_CLOSE:
			status = STATUS_SUCCESS;
			break;

		case IRP_MJ_DEVICE_CONTROL:
			inputBuffer        = Irp->AssociatedIrp.SystemBuffer;
			inputBufferLength  = iosp->Parameters.DeviceIoControl.InputBufferLength;
			outputBuffer       = Irp->AssociatedIrp.SystemBuffer;
			outputBufferLength = iosp->Parameters.DeviceIoControl.OutputBufferLength;
			ioControlCode      = iosp->Parameters.DeviceIoControl.IoControlCode;

			status = MyfaultDeviceControl( iosp->FileObject, TRUE, inputBuffer, inputBufferLength, outputBuffer, outputBufferLength,
				ioControlCode, &Irp->IoStatus, DeviceObject );
			break;
		
		default:
			status = STATUS_INVALID_DEVICE_REQUEST;
			break;        
	}

	// Complete the request
	Irp->IoStatus.Status = status;
	IoCompleteRequest( Irp, IO_NO_INCREMENT );
	return status;
}

//----------------------------------------------------------------------
//
// MyfaultUnload
//
// Our job is done - time to leave.
//
//----------------------------------------------------------------------
VOID MyfaultUnload( IN PDRIVER_OBJECT DriverObject )
{
	WCHAR                   deviceLinkBuffer[]  = DOS_DEVICE_NAME;
	UNICODE_STRING          deviceLinkUnicodeString;

	// Delete the symbolic link for our device
	RtlInitUnicodeString( &deviceLinkUnicodeString, deviceLinkBuffer );
	IoDeleteSymbolicLink( &deviceLinkUnicodeString );

	// Delete the device object
	IoDeleteDevice( DriverObject->DeviceObject );
}

//----------------------------------------------------------------------
//
// DriverEntry
//
// Installable driver initialization. Here we just set ourselves up.
//
//----------------------------------------------------------------------
NTSTATUS DriverEntry(IN PDRIVER_OBJECT DriverObject, IN PUNICODE_STRING RegistryPath)
{
	NTSTATUS                status;
	WCHAR                   deviceNameBuffer[]  = DEVICE_NAME;
	UNICODE_STRING          deviceNameUnicodeString;
	WCHAR                   deviceLinkBuffer[]  = DOS_DEVICE_NAME;
	UNICODE_STRING          deviceLinkUnicodeString;  
	PDEVICE_OBJECT          interfaceDevice = NULL;

	DbgPrint("Starting driver...");

	// Create a named device object
	RtlInitUnicodeString (&deviceNameUnicodeString, deviceNameBuffer );
	status = IoCreateDevice ( DriverObject, 0, &deviceNameUnicodeString, FILE_DEVICE_MYFAULT, 0, TRUE, &interfaceDevice );
	if( NT_SUCCESS(status) )
	{
		// Create a symbolic link that the GUI can specify to gain access
		// to this driver/device
		RtlInitUnicodeString (&deviceLinkUnicodeString, deviceLinkBuffer );
		status = IoCreateSymbolicLink (&deviceLinkUnicodeString, &deviceNameUnicodeString );

		// Create dispatch points for all routines that must be Myfaultd
		DriverObject->MajorFunction[IRP_MJ_CREATE] = DriverObject->MajorFunction[IRP_MJ_CLOSE] =
			DriverObject->MajorFunction[IRP_MJ_DEVICE_CONTROL] = MyfaultDispatch;
		DriverObject->DriverUnload = MyfaultUnload;
	}

	if( !NT_SUCCESS(status) && interfaceDevice )
	// Something went wrong, so clean up 
		IoDeleteDevice( interfaceDevice );
	return status;
}