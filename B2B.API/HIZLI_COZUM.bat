@echo off
chcp 65001 >nul
cls
echo ========================================
echo   HIZLI COZUM - INTERNET KONTROLU
echo ========================================
echo.

echo [1] Internet baglantisi test ediliyor...
ping -n 1 api.nuget.org >nul 2>&1
if %errorlevel% neq 0 (
    echo    [HATA] Internet baglantisi YOK!
    echo.
    echo    COZUM:
    echo    1. Internet baglantini kontrol et
    echo    2. WiFi/Ethernet baglantini kontrol et
    echo    3. Modemi yeniden baslat
    echo.
    pause
    exit /b 1
)
echo    [OK] Internet baglantisi var

echo.
echo [2] Proxy ayarlari temizleniyor...
set HTTP_PROXY=
set HTTPS_PROXY=
set http_proxy=
set https_proxy=
echo    [OK] Proxy temizlendi

echo.
echo [3] NuGet restore deneniyor...
cd /d "%~dp0\.."
dotnet restore 2>&1 | findstr /i "error" >nul
if %errorlevel% equ 0 (
    echo    [HATA] Restore hala basarisiz!
    echo.
    echo    Detayli hata:
    dotnet restore 2>&1 | findstr /i "error"
    echo.
    echo    ALTERNATIF COZUM:
    echo    - Farkli bir internet baglantisi dene (telefon hotspot)
    echo    - VPN kapat
    echo    - Antivirus/firewall gecici olarak kapat
    pause
    exit /b 1
)

echo    [OK] Restore basarili!

echo.
echo [4] Build yapiliyor...
cd B2B.API
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
