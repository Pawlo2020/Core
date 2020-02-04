import sys
import json
from System.Collections.Generic import List


with open("test.json", "r") as read_file:
    print(a)
    loaded = json.load(read_file)
    events = loaded.get('event')
    lista = List[object]()
    for p in loaded['events']:
        lista.Add(p['name'])
        