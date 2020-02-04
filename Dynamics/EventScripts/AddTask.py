import sys
import json

with open(PATH, "r") as read_file:
    data = json.load(read_file)
    i = 0
    while i < len(data["events"]):
        if(data["events"][i]["PID"] == PID):
            data["events"][i]["TASKS"].append({"TID": TID, "TNAME": TNAME, "TSTART": TSTART, "TFINISH": TFINISH, "TFKID": TFKID})
        i+=1
    
with open(PATH, "w") as write_file:
    json.dump(data, write_file, indent=4)
    print(TID)
    print(TNAME)
    print(TSTART)
    print(TFINISH)
    print(TFKID)