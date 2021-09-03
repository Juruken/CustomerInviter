## Customers API

Customers can be added or fetched from the API put and get requests, there are two options for adding customers. Either as a JSON put or you can upload a file such as seen in Data/customers.txt.

The API runs on http://localhost:5000/ by default, it logs to Seq (http://localhost:5341 by default) based on the configuration in settings.json.
- This requires Seq to be installed for you to view it, otherwise it will also log to console

The settings file allows you to specify the Latitude and Longitude of your headquarters.

## Running

The API can be run directly through visual studio launching `CustomerInvite.Api.Service` or you can build and launch the CustomerInvite.Api.Service.exe

### Get All Customers

> **GET** http://localhost:5000/customers

Example response: 

```json
[
    {
        "user_Id": 4,
        "name": "Ian Kehoe",
        "longitude": "-6.238335",
        "latitude": "53.2451022"
    },
    {
        "user_Id": 5,
        "name": "Nora Dempsey",
        "longitude": "-6.2397222",
        "latitude": "53.1302756"
    }
]
```

* **200 OK** - the customer data is returned

> **GET** http://localhost:5000/customers/distance/{distance}
>
> |    Parameter    |    Description                                                  |
> | --------------- | --------------------------------------------------------------- |
> |    distance     |    Distance from Latitude and Longitude of your headquarters in kilometers    |
  
Customer data is returned within the specified distance in kilometers from the configured coordinates in settings.json.
It is sorted by the customers user id asc.

```json
[
    {
        "user_Id": 4,
        "name": "Ian Kehoe",
        "longitude": "-6.238335",
        "latitude": "53.2451022"
    },
    {
        "user_Id": 5,
        "name": "Nora Dempsey",
        "longitude": "-6.2397222",
        "latitude": "53.1302756"
    }
]
```

* **200 OK** - the customer data is returned

> **PUT** http://localhost:5000/customers

Example request:
```json
[
    {"latitude": "52.986375", "user_id": 12, "name": "Christina McArdle", "longitude": "-6.043701"},
    {"latitude": "51.92893", "user_id": 1, "name": "Alice Cahill", "longitude": "-10.27699"}
]
```

* **202 Accepted** - the customer data has been accepted
* **400 BadRequest** - Invalid customer data was received

> **PUT** http://localhost:5000/customers/import

Customers can be directly uploaded by sending one or more application/text files containing JSON.
The JSON is assumed to be on each line, all data will be combined and added

Example File:
```
{"latitude": "52.986375", "user_id": 12, "name": "Christina McArdle", "longitude": "-6.043701"}
{"latitude": "51.92893", "user_id": 1, "name": "Alice Cahill", "longitude": "-10.27699"}
```

* **202 Accepted** - the customer data has been accepted
* **400 BadRequest** - Invalid customer data was received or was unable to successfully ingest any customers