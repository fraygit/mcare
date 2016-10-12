# Parko API

### How to call from angular

*e.g. POST method*

```javascript
	var data = {
		"Firstname": $scope.registration.firstname,
		"Surname": $scope.registration.surname,
		"Email": $scope.registration.email,
		"Password": $scope.registration.password,
		"Address": $scope.registration.address,
		"Lat": $scope.registration.lat,
		"Lng": $scope.registration.lng,
		"PricePerHour": 0,
		"NumberOfVehicle": $scope.registration.numOfVehicle,
		"Availability": availability,
		"Phone" : $scope.registration.phone
	};
	 $http.post('http://core.parko.co.nz/api/ParkingSpace',
				  JSON.stringify(data),
				  {
					  headers: {
						  'Content-Type': 'application/json'
					  }
				  }
			  ).then(function (result) {              
				  // success 

			  }, function (error) {
				// fail
			  });
```


*e.g. PUT method*

```javascript
    var uploadRequest = {
        method: 'PUT',
        url: 'http://core.parko.co.nz/api/ParkingSpace',
        data: fileFormData,
        headers: {
            'Content-Type': undefined
        }
    };

    $http(uploadRequest)
        .success(function (d) {
            // success
        })
        .error(function (ee) {
            // error
        });
```

### Description
Parameters and response are documented on http://core.parko.co.nz/swagger/ui/index.
This is just a short description of what the function of each Url and method

### /api/ParkingSpace

*GET* - get all the parking spaces details

*POST* - register a parking space

*PUT* - upload image of a parking space


### /api/User

*GET* - signup

*POST* - login


### /api/Booking


*POST* - book a carpark


### /api/AdminUser

*POST* - admin user login

### /api/ParkingSpaceDetails
*GET* - Get details of specific parking space

*POST* - Update parking space


### /api/ParkingSpaceImage
*GET* - Get parking space image


### Parking Space Status

1 = default, not approved.

0 = approved.

