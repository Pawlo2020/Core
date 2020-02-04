import sys
import json

with open(PATH, "r") as read_file:
    data = json.load(read_file)
i = 0
while i < len(data["events"]):
    if data["events"][i]["PID"] == PID:
        j=0
        while j< len(data["events"][i]["TASKS"]):
            if data["events"][i]["TASKS"][j]["TID"] == TID:
                del data["events"][i]["TASKS"][j]
                break  
            j+=1
    i+=1

with open(PATH, "w") as write_file:
    json.dump(data, write_file, indent=4)