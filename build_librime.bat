cd librime
call env.bat
set PATH=%PATH%;%CMAKE_INSTALL_PATH%\bin
call "%VS_INSTALL_PATH%\VC\vcvarsall.bat" x86
call build.bat boost
call build.bat thirdparty
call build.bat librime
copy build\lib\Release\rime.dll ..\YIME\rime.dll
