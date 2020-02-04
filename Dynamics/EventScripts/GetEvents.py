import sys
import json
from System.Collections.Generic import List, Dictionary
IDdyn = List[object]()
NAMEdyn = List[object]()
STARTdyn = List[object]()
FINISHdyn = List[object]()
PDICT = Dictionary[object, List[object]]()

def IDLIST(data):
    i=0
    while i < len(data["events"]):
        IDdyn.Add(data["events"][i]["PID"])
        i+=1
    PDICT.Add("PID", IDdyn)

def NAMELIST(data):
    i=0
    while i < len(data["events"]):
        NAMEdyn.Add(data["events"][i]["PNAME"])
        i+=1
    PDICT.Add("PNAME", NAMEdyn)

def STARTLIST(data):
    i=0
    while i < len(data["events"]):
        STARTdyn.Add(data["events"][i]["PSTART"])
        i+=1
    PDICT.Add("PSTART", STARTdyn)

def FINISHLIST(data):
    i=0
    while i < len(data["events"]):
        FINISHdyn.Add(data["events"][i]["PFINISH"])
        i+=1
    PDICT.Add("PFINISH", FINISHdyn)

def Worker(data):
    IDLIST(data)
    NAMELIST(data)
    STARTLIST(data)
    FINISHLIST(data)
    return PDICT

with open(PATH) as readable:
        dataset = json.load(readable)
        Count = len(dataset["events"])
        Worker(dataset)
        