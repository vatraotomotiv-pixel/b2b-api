@echo off
chcp 65001 >nul
cls
echo ========================================
echo   DATABASE OLUSTUR
echo ========================================
echo.

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
echo [2/3] Database Olusturuluyor...
C:\xampp\mysql\bin\mysql.exe -u root -e "CREATE DATABASE IF NOT EXISTS b2b_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;" 2>nul
if %errorlevel% neq 0 (
    echo    [HATA] Database olusturulamadi!
    echo    MySQL'e manuel baglan ve su komutu calistir:
    echo    CREATE DATABASE b2b_db CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
    pause
    exit /b 1
)
echo    [OK] Database olusturuldu

echo.
echo [3/3] Migration Calistiriliyor...
cd /d "%~dp0\B2B.API"
dotnet ef database update --project ..\B2B.Infrastructure --startup-project . 2>nul
if %errorlevel% neq 0 (
    echo    [UYARI] Migration calistirilamadi!
    echo    EF Core tool yuklu degil olabilir.
    echo    Manuel olarak: dotnet ef database update
    echo.
    echo    Ama database olusturuldu, backend calisabilir!
    pause
    exit /b 0
)
echo    [OK] Migration basarili!

echo.
echo ========================================
echo   DATABASE HAZIR!
echo ========================================
echo.
echo   Simdi backend'i calistirabilirsin (F5)
echo.
pause
