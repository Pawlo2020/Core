import sys
import json

data = {
    "events":
    [

    ]
}

with open(PATH, "w") as write_file:
    json.dump(data, write_file, indent=4)