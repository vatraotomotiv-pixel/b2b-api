@echo off
chcp 65001 >nul
cls
echo ========================================
echo   SON DENEME - TUM ADIMLAR
echo ========================================
echo.

cd /d "%~dp0"

echo [1] Internet test...
ping -n 1 8.8.8.8 >nul
if %errorlevel% neq 0 (
    echo    [HATA] Internet YOK!
    pause
    exit /b 1
)
echo    [OK] Internet var

echo.
echo [2] NuGet cache temizle...
dotnet nuget locals all --clear >nul 2>&1
echo    [OK] Cache temizlendi

echo.
echo [3] Restore yap...
cd B2B.API
dotnet restore
if %errorlevel% neq 0 (
    echo.
    echo    [HATA] Restore BASARISIZ!
    echo.
    echo    MANUEL DENEME:
    echo    1. Visual Studio'yu ac
    echo    2. B2B.sln dosyasini ac
    echo    3. Solution'a sag tikla - Restore NuGet Packages
    echo.
    pause
    exit /b 1
)

echo.
echo [4] Build yap...
dotnet build
if %errorlevel% neq 0 (
    echo    [HATA] Build basarisiz!
    pause
    exit /b 1
)

echo.
echo ========================================
echo   BASARILI! Backend hazir!
echo ========================================
echo.
echo   Simdi BASLA.bat dosyasina cift tikla
echo.
pause
