@echo off
cls

.paket\paket.bootstrapper.exe
if errorlevel 1 (
	exit /b %errorlevel%
)

.paket\paket.exe restore
if errorlevel 1 (
	exit /b %errorlevel%
)

packages\build\FAKE\tools\FAKE.exe build.fsx
rmdir packages /S /Q
rmdir .fake /S /Q
del build.cmd /F /Q