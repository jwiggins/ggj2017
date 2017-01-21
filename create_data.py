import sys
import yaml

with open('data.yaml', 'r') as ifp:
    data = yaml.load(ifp.read())

seasons = data['Object']['_seasons']

with open(sys.argv[1], 'w') as ofp:
    for s in seasons:
        print(s['_minTemperature'], s['_maxTemperature'], file=ofp)
        for w in s['_weathers']:
            print(w['_probability'], w['_temperature'], w['_moisture'], file=ofp)
