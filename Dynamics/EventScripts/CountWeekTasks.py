import sys
import json
from datetime import datetime
from System.Collections.Generic import List

WeekTasks = List[int]()
Counter = [0,0,0,0,0,0,0]

def Convert(date):
    return datetime.strptime(date, '%d.%m.%Y')

def Count(data):
    mainiterator = 0
    while(mainiterator<len(data["events"])):
        taskiterator = 0
        while taskiterator < len(data["events"][mainiterator]["TASKS"]):
            Tasks = data["events"][mainiterator]["TASKS"][taskiterator]
            if Convert(Tasks["TFINISH"])>= Convert(WeekList[0]):
                i=0
                while i < len(Counter):
                    if ((Convert(Tasks["TSTART"]) <= Convert(WeekList[i]) and Convert(Tasks["TFINISH"]) >= Convert(WeekList[i])) or (Convert(Tasks["TFINISH"]) ==  Convert(WeekList[i])) or (Convert(Tasks["TSTART"]) == Convert(WeekList[i]) )):
                        Counter[i]+=1
                    i+=1
            taskiterator += 1
        mainiterator += 1

with open(PATH, 'r') as read_file:
    data = json.load(read_file)
    Count(data)
    i=0
    while i < len(Counter):
        WeekTasks.Add(Counter[i])
        i+=1
