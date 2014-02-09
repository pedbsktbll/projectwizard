/*
 * Filename:		_____FILENAME_____.c
 *
 * Author:			_____USER_____
 * Date Created:	_____DATE_____
 * Version _____VERSION_____:		_____DATE_____ (_____USER_____)
 *
 * _____DESCRIPTION_____
 *
 */

/*
	Driver_Template - Main file
	This file contains a very simple implementation of a WDM driver. Note that it does not support all
	WDM functionality, or any functionality sufficient for practical use. The only thing this driver does
	perfectly, is loading and unloading.

	To install the driver, go to Control Panel -> Add Hardware Wizard, then select "Add a new hardware device".
	Select "manually select from list", choose device category, press "Have Disk" and enter the path to your
	INF file.
	Note that not all device types (specified as Class in INF file) can be installed that way.

	To start/stop this driver, use Windows Device Manager (enable/disable device command).

	If you want to speed up your driver development, it is recommended to see the BazisLib library, that
	contains convenient classes for standard device types, as well as a more powerful version of the driver
	wizard. To get information about BazisLib, see its website:
		http://bazislib.sysprogs.org/
*/
#include <ntddk.h>
#include <ntddstor.h>
#include <mountdev.h>
#include <ntddvol.h>


void Driver_TemplateUnload(IN PDRIVER_OBJECT DriverObject);
NTSTATUS Driver_TemplateCreateClose(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp);
NTSTATUS Driver_TemplateDefaultHandler(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp);
NTSTATUS Driver_TemplateAddDevice(IN PDRIVER_OBJECT  DriverObject, IN PDEVICE_OBJECT  PhysicalDeviceObject);
NTSTATUS Driver_TemplatePnP(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp);

typedef struct _deviceExtension
{
	PDEVICE_OBJECT DeviceObject;
	PDEVICE_OBJECT TargetDeviceObject;
	PDEVICE_OBJECT PhysicalDeviceObject;
	UNICODE_STRING DeviceInterface;
} Driver_Template_DEVICE_EXTENSION, *PDriver_Template_DEVICE_EXTENSION;

// {5e579749-2f11-49a5-9049-5682332a9ab3}
static const GUID GUID_Driver_TemplateInterface = {0x5E579749, 0x2f11, 0x49a5, {0x90, 0x49, 0x56, 0x82, 0x33, 0x2a, 0x9a, 0xb3 } };

#ifdef __cplusplus
extern "C" NTSTATUS DriverEntry(IN PDRIVER_OBJECT DriverObject, IN PUNICODE_STRING  RegistryPath);
#endif

NTSTATUS DriverEntry(IN PDRIVER_OBJECT DriverObject, IN PUNICODE_STRING  RegistryPath)
{
	unsigned i;

	DbgPrint("Hello from Driver_Template!\n");
	
	for (i = 0; i <= IRP_MJ_MAXIMUM_FUNCTION; i++)
		DriverObject->MajorFunction[i] = Driver_TemplateDefaultHandler;

	DriverObject->MajorFunction[IRP_MJ_CREATE] = Driver_TemplateCreateClose;
	DriverObject->MajorFunction[IRP_MJ_CLOSE] = Driver_TemplateCreateClose;
	DriverObject->MajorFunction[IRP_MJ_PNP] = Driver_TemplatePnP;

	DriverObject->DriverUnload = Driver_TemplateUnload;
	DriverObject->DriverStartIo = NULL;
	DriverObject->DriverExtension->AddDevice = Driver_TemplateAddDevice;

	return STATUS_SUCCESS;
}

void Driver_TemplateUnload(IN PDRIVER_OBJECT DriverObject)
{
	DbgPrint("Goodbye from Driver_Template!\n");
}

NTSTATUS Driver_TemplateCreateClose(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp)
{
	Irp->IoStatus.Status = STATUS_SUCCESS;
	Irp->IoStatus.Information = 0;
	IoCompleteRequest(Irp, IO_NO_INCREMENT);
	return STATUS_SUCCESS;
}

NTSTATUS Driver_TemplateDefaultHandler(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp)
{
	PDriver_Template_DEVICE_EXTENSION deviceExtension = NULL;
	
	IoSkipCurrentIrpStackLocation(Irp);
	deviceExtension = (PDriver_Template_DEVICE_EXTENSION) DeviceObject->DeviceExtension;
	return IoCallDriver(deviceExtension->TargetDeviceObject, Irp);
}

NTSTATUS Driver_TemplateAddDevice(IN PDRIVER_OBJECT  DriverObject, IN PDEVICE_OBJECT  PhysicalDeviceObject)
{
	PDEVICE_OBJECT DeviceObject = NULL;
	PDriver_Template_DEVICE_EXTENSION pExtension = NULL;
	NTSTATUS status;
	
	status = IoCreateDevice(DriverObject,
						    sizeof(Driver_Template_DEVICE_EXTENSION),
							NULL,
							FILE_DEVICE_UNKNOWN,
							0,
							0,
							&DeviceObject);

	if (!NT_SUCCESS(status))
		return status;

	pExtension = (PDriver_Template_DEVICE_EXTENSION)DeviceObject->DeviceExtension;

	pExtension->DeviceObject = DeviceObject;
	pExtension->PhysicalDeviceObject = PhysicalDeviceObject;
	pExtension->TargetDeviceObject = IoAttachDeviceToDeviceStack(DeviceObject, PhysicalDeviceObject);

	status = IoRegisterDeviceInterface(PhysicalDeviceObject, &GUID_Driver_TemplateInterface, NULL, &pExtension->DeviceInterface);
	ASSERT(NT_SUCCESS(status));

	DeviceObject->Flags &= ~DO_DEVICE_INITIALIZING;
	return STATUS_SUCCESS;
}


NTSTATUS Driver_TemplateIrpCompletion(
					  IN PDEVICE_OBJECT DeviceObject,
					  IN PIRP Irp,
					  IN PVOID Context
					  )
{
	PKEVENT Event = (PKEVENT) Context;

	UNREFERENCED_PARAMETER(DeviceObject);
	UNREFERENCED_PARAMETER(Irp);

	KeSetEvent(Event, IO_NO_INCREMENT, FALSE);

	return(STATUS_MORE_PROCESSING_REQUIRED);
}

NTSTATUS Driver_TemplateForwardIrpSynchronous(
							  IN PDEVICE_OBJECT DeviceObject,
							  IN PIRP Irp
							  )
{
	PDriver_Template_DEVICE_EXTENSION   deviceExtension;
	KEVENT event;
	NTSTATUS status;

	KeInitializeEvent(&event, NotificationEvent, FALSE);
	deviceExtension = (PDriver_Template_DEVICE_EXTENSION) DeviceObject->DeviceExtension;

	IoCopyCurrentIrpStackLocationToNext(Irp);

	IoSetCompletionRoutine(Irp, Driver_TemplateIrpCompletion, &event, TRUE, TRUE, TRUE);

	status = IoCallDriver(deviceExtension->TargetDeviceObject, Irp);

	if (status == STATUS_PENDING) {
		KeWaitForSingleObject(&event, Executive, KernelMode, FALSE, NULL);
		status = Irp->IoStatus.Status;
	}
	return status;
}

NTSTATUS Driver_TemplatePnP(IN PDEVICE_OBJECT DeviceObject, IN PIRP Irp)
{
	PIO_STACK_LOCATION irpSp = IoGetCurrentIrpStackLocation(Irp);
	PDriver_Template_DEVICE_EXTENSION pExt = ((PDriver_Template_DEVICE_EXTENSION)DeviceObject->DeviceExtension);
	NTSTATUS status;

	ASSERT(pExt);

	switch (irpSp->MinorFunction)
	{
	case IRP_MN_START_DEVICE:
		IoSetDeviceInterfaceState(&pExt->DeviceInterface, TRUE);
		Irp->IoStatus.Status = STATUS_SUCCESS;
		IoCompleteRequest(Irp, IO_NO_INCREMENT);
		return STATUS_SUCCESS;

	case IRP_MN_QUERY_REMOVE_DEVICE:
		Irp->IoStatus.Status = STATUS_SUCCESS;
		IoCompleteRequest(Irp, IO_NO_INCREMENT);
		return STATUS_SUCCESS;

	case IRP_MN_REMOVE_DEVICE:
		IoSetDeviceInterfaceState(&pExt->DeviceInterface, FALSE);
		status = Driver_TemplateForwardIrpSynchronous(DeviceObject, Irp);
		IoDetachDevice(pExt->TargetDeviceObject);
		IoDeleteDevice(pExt->DeviceObject);
		RtlFreeUnicodeString(&pExt->DeviceInterface);
		Irp->IoStatus.Status = STATUS_SUCCESS;
		IoCompleteRequest(Irp, IO_NO_INCREMENT);
		return STATUS_SUCCESS;

	case IRP_MN_QUERY_PNP_DEVICE_STATE:
		status = Driver_TemplateForwardIrpSynchronous(DeviceObject, Irp);
		Irp->IoStatus.Information = 0;
		IoCompleteRequest(Irp, IO_NO_INCREMENT);
		return status;
	}
	return Driver_TemplateDefaultHandler(DeviceObject, Irp);
}