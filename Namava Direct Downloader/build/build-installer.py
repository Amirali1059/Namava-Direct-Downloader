import os
from subprocess import call, Popen, getoutput
from shutil import rmtree, copy2
Setup="Namava Direct Downloader.exe"
name = "Namava Direct Downloader V2.4.2"
def Print(txt,tabs):
    t=" -->"+" "*(((tabs-1)*4)-1)
    if tabs != 0:print(t,txt)
    else: print(txt)
PerBuild=1
FreeBuild=False
ThisPath=os.path.abspath(os.getcwd())
SetupPath = "Release\\"
Print('building installer:',1)
ins=open("installer.nsi","rb").read().replace(b"{Name}",name.encode())
open("ins.nsi","wb").write(ins)
call('makensis /V1 ins.nsi')
os.remove("ins.nsi")
# ------------------------------------------------------------------
os.system('pause')