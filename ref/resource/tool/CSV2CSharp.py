# -*- coding: utf-8 -*-
#cunkai
import os 
import shutil
import re
import csv
import platform
import sys
import codecs

reload(sys)
sys.setdefaultencoding("utf-8")

sysType = platform.system()
ESC = chr(27)
# 字体
a_default  = 0
a_bold     = 1
a_italic   = 3
a_underline= 4
# 前景颜色
fg_black   = 30
fg_red     = 31
fg_green   = 32
fg_yellow  = 33
fg_blue    = 34
fg_magenta = 35
fg_cyan    = 36
fg_white   = 37
# 后景颜色
bg_black   = 40
bg_red     = 41
bg_green   = 42
bg_yellow  = 43
bg_blue    = 44
bg_magenta = 45
bg_cyan    = 46
bg_white   = 47
 
def color_code(a):
    return ESC+"[%dm"%a
    
def color_str(s, *args):
    if(sysType =="Windows"):
        return s
    cs = ""
    for a in args:
        cs += color_code(a)
    cs += s
    cs += color_code(a_default)
    return cs 



print("\n\n\n\n\n\n\n\n\n")

path = os.path.split(os.path.realpath(__file__))[0]
csvPath = os.path.dirname(path)+"/csv/"
csharp = os.path.dirname(path)+"/csharp/"
csvDataParentName = "CsvDataParent"
#dataManageName = "dataManage"

defaultcs = path+"/default/defaultclass.cs"
defaultcsv = path+"/default/defaultcsv.csv"
#defaultDataManage = path+"/default/defaultDataManage.cs"
defaultCsvDataParent = path+"/default/defaultCsvDataParent.cs"

print(color_str("SYS: "+sysType,bg_blue, bg_cyan, a_bold))
print ("coding: "+sys.getdefaultencoding())

classList = []

def toCsString(line):
    isItem = 0

    data = '\"'

    for i in range(len(line)):
        item = line[i]
        if item != '':
            isItem = 1
            data += item
            if i<len(line)-1:
                data += '|'
        else:
        	data += '0'
        	if i<len(line)-1:
        		data+='|'

    data+='\"'

    if isItem==0:
        return ''
    return data

def openFile(name,ftype):
    fp=None

    if(sysType =="Windows"):
        try:
            fp=open(name,ftype,encoding='utf-8')
        except:  
            fp=open(name,ftype)
    else:
        fp=open(name,ftype)

    return fp

def mkdir(path):

    path=path.strip()

    path=path.rstrip("\\")

    isExists=os.path.exists(path)
 
    if not isExists:
        # print path+' OK'
        os.makedirs(path)
        return True
    else:
        # print path+'OK'
        return False

def toIfFromClass(name):
    code = "\t\t\tif(className==\""+name+"\")\n\t\t\t{"+"\n\t\t\t\t"+"return "+name+".Instance();"+"\n\t\t\t}"
    return code

def start(rootDir): 
    list_dirs = os.walk(rootDir)

    # remove old csharp
    isExists=os.path.exists(csharp)
    if isExists:
        shutil.rmtree(csharp)

    for root, dirs, files in list_dirs: 
        for f in files: 
            filePath = os.path.join(root, f) 
            fname, fextension = os.path.splitext(f)
            csharpFilePath = os.path.join(csharp, "data/"+fname)+".cs"
            # csharpFilePath = csharpFilePath.replace(csvPath,csharp)
            if fextension==".csv":
              
                # print(f)
                print(color_str(" -- "+f,fg_cyan, bg_black, a_bold))
                mkdir(csharp+"data")
                # copy default.cs
                shutil.copy(defaultcs,csharpFilePath)

                #add classList
                classList.append(fname)

                #；compatible
                fp = None

                fp=openFile(filePath,'r')
                alllines=fp.readlines()
                fp.close() 
                fp=openFile(defaultcsv,'w')


                #write start
                for eachline in alllines:
                    #；compatible
                    a=re.sub(';',',',eachline)
                    fp.writelines(a) 
                fp.close()
  
                if(sysType == "Windows"):
                    try:
                        reader = csv.reader(open(defaultcsv, 'rU',encoding='utf-8'), dialect='excel')
                    except:
                    	reader = csv.reader(open(defaultcsv, 'rU'), dialect='excel')
                else:
                    reader = csv.reader(open(defaultcsv, 'rU'), dialect='excel')

                chinaKey = None
                key = None
                data = ''

                for line in reader:
                    if chinaKey is None:
                        chinaKey = toCsString(line)
                    else:
                        if key is None:
                            key = toCsString(line)
                            print (key)
                        else:
                            item = toCsString(line)
                            if item!='':
                                data = data +item+ ',\n\t\t'
                if(sysType == "Windows"):               
                    chinaKey = str(chinaKey).decode('gb2312')
                    key = str(key).decode('gb2312')
                    data = str(data).decode('gb2312')
                #print(key)
                #print(data)
                # write *.cs
                # read lines
                fp = None

                fp=openFile(csharpFilePath,'r')
                alllines=fp.readlines()
                fp.close() 
                fp=openFile(csharpFilePath,'w')

                #write start
                for eachline in alllines:
                    # class name
                    a=re.sub('defaultclass',fname,eachline)
                    # china key
                    a=re.sub('defaultChinakey',chinaKey,a)
                    # allkey
                    a=re.sub('\"//defaultkey\"',key,a)
                    # alldata
                    a=re.sub('//defaultdata',data,a)
                    # csvDataParent
                    a=re.sub('defaultCsvDataParent',csvDataParentName,a)
                    fp.writelines(a)
                fp.close()


# copy defaultCsvDataParent.cs
    csvDataParentPath = csharp+csvDataParentName+".cs"
    shutil.copy(defaultCsvDataParent,csvDataParentPath)

    fp = None
    
    fp=openFile(csvDataParentPath,'r')
    alllines=fp.readlines()
    fp.close() 
    fp=openFile(csvDataParentPath,'w')

    #write start
    for eachline in alllines:
        # class name
        a=re.sub('defaultCsvDataParent',csvDataParentName,eachline)
        fp.writelines(a) 
    fp.close()

    # print(classList)

                
start(csvPath)
print(color_str("GOOD",fg_white, bg_green, a_italic))
#coding=utf-8
if(sysType =="Windows"):
    raw_input(unicode('按回车键退出...','utf-8').encode('gbk'))
else:
    raw_input(unicode('按回车键退出...','utf-8').encode('utf-8'))