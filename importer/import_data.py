
# Lets Python make HTTP requests to the Chicago 311 API.
import requests

# Lets Python connect to MySQL.
import mysql.connector


# URL for the Chicago 311 API.
API_URL = "https://data.cityofchicago.org/resource/v6vf-nfxy.json"


# Ask the API for 1,000 recent records.
params = {
    "$limit": 1000,

    # Only request the columns our database uses.
    "$select": "sr_number,sr_type,sr_short_code,owner_department,status,origin,created_date,last_modified_date,closed_date,street_address,city,state,zip_code,community_area,ward,latitude,longitude",

    # Sort by newest requests first.
    "$order": "created_date DESC"
}


# Send a GET request to the Chicago 311 API.
response = requests.get(API_URL, params=params)

# Stop the program if the API returned an error.
response.raise_for_status()

# Convert the API response from JSON into Python data.
data = response.json()

# Print how many records we received.
print(f"Received {len(data)} records.")


# Connect to our MySQL database.
connection = mysql.connector.connect(
    host="localhost",
    user="root",
    database="chicago_311"
)

# Create a cursor for executing SQL commands.
cursor = connection.cursor()


# Get the first service request from the API.
request = data[0]


# SQL command for inserting or updating a service request.
sql = """
INSERT INTO service_requests (
    sr_number,
    sr_type,
    sr_short_code,
    owner_department,
    status,
    origin,
    created_date,
    last_modified_date,
    closed_date,
    street_address,
    city,
    state,
    zip_code,
    community_area,
    ward,
    latitude,
    longitude
)
VALUES (
    %s, %s, %s, %s, %s, %s, %s, %s, %s,
    %s, %s, %s, %s, %s, %s, %s, %s
)
ON DUPLICATE KEY UPDATE
    sr_type = VALUES(sr_type),
    sr_short_code = VALUES(sr_short_code),
    owner_department = VALUES(owner_department),
    status = VALUES(status),
    origin = VALUES(origin),
    created_date = VALUES(created_date),
    last_modified_date = VALUES(last_modified_date),
    closed_date = VALUES(closed_date),
    street_address = VALUES(street_address),
    city = VALUES(city),
    state = VALUES(state),
    zip_code = VALUES(zip_code),
    community_area = VALUES(community_area),
    ward = VALUES(ward),
    latitude = VALUES(latitude),
    longitude = VALUES(longitude)
"""


# Insert every service request we received.
for request in data:

    # Values that will be inserted into MySQL.
    values = (
        request.get("sr_number"),
        request.get("sr_type"),
        request.get("sr_short_code"),
        request.get("owner_department"),
        request.get("status"),
        request.get("origin"),
        request.get("created_date"),
        request.get("last_modified_date"),
        request.get("closed_date"),
        request.get("street_address"),
        request.get("city"),
        request.get("state"),
        request.get("zip_code"),
        request.get("community_area"),
        request.get("ward"),
        request.get("latitude"),
        request.get("longitude")
    )

    # Execute the SQL command for this request.
    cursor.execute(sql, values)


# Save all database changes.
connection.commit()

# Tell us how many records we processed.
print(f"Processed {len(data)} records.")

# Tell us which record was inserted.
print(f"Inserted {request.get('sr_number')}.")


# Close the cursor.
cursor.close()

# Close the database connection.
connection.close()