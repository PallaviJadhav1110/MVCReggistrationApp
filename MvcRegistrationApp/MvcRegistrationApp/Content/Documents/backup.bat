echo off
cls
echo -- BACKUP DATABASE --
set  DATABASENAME=UserInfoDatabase

:: filename format Name-Date (eg MyDatabase-2009.5.19.bak)
set DATESTAMP=%DATE:~-4%.%DATE:~7,2%.%DATE:~4,2%
set BACKUPFILENAME=E:P:\Users\fwin001002\Documents\Visual Studio 2012\backup\Test1.bak //FolderName
set SERVERNAME=(local)
set UN=sa
set PWD=fulcrum#1
echo.

sqlcmd  -S %SERVERNAME%  -U %UN% -P%PWD%  -d DataBaseName  -Q "BACKUP DATABASE [%DATABASENAME%] TO DISK = N'%BACKUPFILENAME%' WITH INIT , NOUNLOAD , NAME = N'%DATABASENAME% backup', NOSKIP , STATS = 10, NOFORMAT"
echo.
pause