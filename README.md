### Brief

We have some customer records in a text file `./Data/customers.txt` -- one customer per line, JSON lines formatted. We want to invite any customer within 100km of our office for some food and drinks on us.

### Tasks

Write a web service that:

-   Has an endpoint that accepts a .txt file containting customers
    -   Read the full list of customers
    -   Output the names and user ids of matching customers (within 100km), sorted by User ID (ascending). The output should be in JSON
-   No authentication is required
-   The GPS coordinates for our Dublin office are 53.339428, -6.257664
