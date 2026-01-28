@echo off
chcp 65001 >nul
cls
echo ========================================
echo   BUILD HATASI DUZELT
echo ========================================
echo.

cd /d "%~dp0\.."

echo [1/4] NuGet Cache Temizleniyor...
dotnet nuget locals all --clear >nul 2>&1
echo    [OK] Cache temizlendi

echo.
echo [2/4] NuGet Paketleri Restore Ediliyor...
dotnet restore --no-cache 2>&1 | findstr /i "error" >nul
if %errorlevel% equ 0 (
    echo    [HATA] Restore basarisiz! Internet baglantini kontrol et.
    echo.
    echo    Cozum:
    echo    1. Internet baglantini kontrol et
    echo    2. VPN varsa kapat
    echo    3. Firewall ayarlarini kontrol et
    pause
    exit /b 1
)
echo    [OK] Paketler restore edildi

echo.
echo [3/4] Build Yapiliyor...
cd B2B.API
dotnet build 2>&1 | findstr /i "error" >nul
if %errorlevel% equ 0 (
    echo    [HATA] Build basarisiz!
    echo.
    echo    Detayli hata icin: dotnet build
    pause
    exit /b 1
)
echo    [OK] Build basarili!

echo.
echo [4/4] Sonuc
echo.
echo ========================================
echo   BUILD BASARILI!
echo ========================================
echo.
echo   Simdi backend'i baslatabilirsin:
echo   - BASLA.bat dosyasina cift tikla
echo   - VEYA: dotnet run
echo.
echo ========================================
echo.

pause
