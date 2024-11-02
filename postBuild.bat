@echo off
setlocal

REM Check if the correct number of arguments is provided
if "%~1"=="" (
    echo Usage: copy_file.bat source_file target_path
    exit /b 1
)

if "%~2"=="" (
    echo Usage: copy_file.bat source_file target_path
    exit /b 1
)

REM Set the source and target paths from arguments
set "source_file=%~1"
set "target_file=%~2"

REM Check if the source file exists
if not exist "%source_file%" (
    echo Error: The source file "%source_file%" does not exist.
    exit /b 1
)

REM Delete the target file if it exists
if exist "%target_file%" (
    echo Deleting existing target file "%target_file%"...
    del "%target_file%"
)

REM Copy the source file to the target location
echo Copying "%source_file%" to "%target_file%"...
copy "%source_file%" "%target_file%"

REM Check if the copy was successful
if %errorlevel%==0 (
    echo File copied successfully.
) else (
    echo Error: Failed to copy file.
)

endlocal
