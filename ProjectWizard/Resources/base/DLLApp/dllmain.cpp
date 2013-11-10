/*
 * Filename:		ConsoleAppTemplate.cpp
 *
 * Author:			<Your name>
 * Date Created:	9/27/2013
 * Version 1.0:		9/27/2013 (Josh)
 *
 * ConsoleApp Template
 */

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
#include <windows.h>

BOOL APIENTRY DllMain( HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved )
{
	switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

