FROM mongo

COPY init.json /init.json
CMD mongoimport --host mongo --db userdb --collection users --drop --file /init.json --jsonArray