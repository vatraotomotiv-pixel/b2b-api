@echo off
chcp 65001 >nul
cls
echo ========================================
echo   B2B PROJESI PUBLISH
echo ========================================
echo.

cd /d "%~dp0\B2B.API"

echo [1/3] Build Yapiliyor...
dotnet build -c Release >nul 2>&1
if %errorlevel% neq 0 (
    echo    [HATA] Build basarisiz!
    pause
    exit /b 1
)
echo    [OK] Build basarili

echo.
echo [2/3] Publish Yapiliyor...
if exist "publish" (
    rmdir /s /q "publish"
)
dotnet publish -c Release -o ./publish
if %errorlevel% neq 0 (
    echo    [HATA] Publish basarisiz!
    pause
    exit /b 1
)
echo    [OK] Publish basarili

echo.
echo [3/3] Dosyalar Hazir
echo.
echo ========================================
echo   PUBLISH KLASORU: B2B.API\publish\
echo ========================================
echo.
echo   Bu klasordeki dosyalari FTP ile yukle:
echo   - httpdocs/ veya public_html/
echo.
echo   Domain: b2b.vatraotomotiv.com.tr
echo.
echo ========================================
echo.

pause
