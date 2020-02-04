import sys
import json

with open(PATH, "r") as read_file:
    data = json.load(read_file)
    data["events"].append({"PID":PID, "PNAME":PNAME, "PSTART":PSTART, "PFINISH":PFINISH, "TASKS":[]})
    with open(PATH, "w") as write_file:
        json.dump(data, write_file, indent=4)
        




