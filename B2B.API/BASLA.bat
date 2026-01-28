@echo off
chcp 65001 >nul
cls
echo ========================================
echo   B2B BACKEND BASLAT
echo ========================================
echo.

cd /d "%~dp0"

echo [1/3] MySQL Kontrolu...
tasklist | find /i "mysqld.exe" >nul
if %errorlevel% neq 0 (
    echo    [HATA] MySQL CALISMIYOR!
    echo    XAMPP Control Panel'den MySQL'i baslat!
    pause
    exit /b 1
)
echo    [OK] MySQL calisiyor

echo.
echo [2/3] Build Yapiliyor...
dotnet build >nul 2>&1
if %errorlevel% neq 0 (
    echo    [HATA] Build basarisiz! Hatalari gormek icin: dotnet build
    pause
    exit /b 1
)
echo    [OK] Build basarili

echo.
echo [3/3] Backend Baslatiliyor...
echo.
echo ========================================
echo   BACKEND HATALARI BURADA GORUNECEK
echo ========================================
echo.
echo   Swagger: http://localhost:5000/swagger
echo   API: http://localhost:5000/api/products
echo   Durdurmak: Ctrl+C
echo.
echo ========================================
echo.

dotnet run

pause
