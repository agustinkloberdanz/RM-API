# RoutingMaps docs

## Index

- [DTOs](#dtos)
- [Controllers](#controllers)

# DTOs

- [Device](##device)
- [Location](#location)
- [Response](##response)
- [Route](##route)
- [User](##user)

## Device

### DeviceDTO

- DeviceKey: string
- DeviceId: string
- Token: string

## Location

### AddLocationDTO

- Address: string
- Order: number
- UserEmail: string
- isNotified: Boolean
- RouteId: number

### LocationDTO

- Id: number
- Address: string
- Order: number
- UserEmail: string
- isNotified: Boolean

## Response

### Response

- StatusCode: number
- Message: string
- IsSuccess: Boolean

### ResponseModel(T)

- StatusCode: number
- Message: string
- IsSuccess: Boolean
- Model: T

### ResponseCollection(T)

- StatusCode: number
- Message: string
- IsSuccess: Boolean
- Collection: List(T)

## Route

### AddRoute

- DrivingDate: DateTime

### RouteDTO

- Id: number
- DrivingDate: DateTime
- Locations: List(LocationDTO)

## User

### UserDTO

- Email: string
- FirstName: string
- LastName: string
- Devices: List(DeviceDTO)

### UserGetOwnDTO

- Email: string
- FirstName: string
- LastName: string
  
### UserLoginDTO

- Email: string
- Password: string

### UserNotificationDTO

- UserEmail: string
- Message: string

### UserRegisterDTO

- Email: string
- FirstName: string
- LastName: string
- Password: string


# Controllers

- [AuthController](##authcontroller)
- [RoutesController](##routescontroller)
- [UsersController](##userscontroller)

## AuthController

### Login ( model: UserLoginDTO )

- Request type: HttpPost
- Endpoint: api/Auth/login
- Result: Json(ResponseModel(string))
- Explanation: This methods pairs the given user data with the database users and return a token.


## RoutesController

### AddLocation (model: AddLocationDTO)
- Request: HttpPost
- Endpoint: api/Routes/location/addLocation
- Result: Json(Response)
- Explanation: This method adds a location to the database.

### DeleteLocation (id: number)
- Request: HttpGet
- Endpoint: api/Routes/location/deleteLocation/{id}
- Result: Json(Response)
- Explanation: This method deletes a location from the database.

### DeleteAllLocationsByRouteId (RouteId: number)
- Request: HttpGet
- Endpoint: api/Routes/location/deleteAllLocations/{RouteId}
- Result: Json(Response)
- Explanation: This method deletes all locationes by its RouteId.

### NotifyLocation(number: id)
- Request: HttpGet
- Endpoint: api/Routes/location/NotifyLocation/id
- Result: Json(Response)
- Explanation: This method sets true the isNotified boolean in Location.

### GetAllRoutes ()
- Request: HttpGet
- Endpoint: api/Routes/getAllRoutes
- Result: Json(ResponseCollection(RouteDTO))
- Explanation: This method gets all the route.

### GetRouteById (id: number)
- Request: HttpGet
- Endpoint: api/Routes/route/getRoute/{id}
- Result: Json(ResponseModel(RouteDTO))
- Explanation: This method gets the route by its id.

### AddRoute (model: AddRouteDTO)
- Request: HttpPost
- Endpoint: api/Routes/AddRoute
- Result: Json(Response)
- Explanation: This method adds an Route to the database.

### UpdateRoute (model: UpdateRouteDTO)
- Request: HttpPost
- Endpoint: api/Routes/route/UpdateRoute
- Result: Json(Response)
- Explanation: This method updates an Route in the database.

### DeleteRoute (id: number)
- Request: HttpGet
- Endpoint: api/Routes/deleteRoute/{id}
- Result: Json(Response)
- Explanation: This method deletes an Route from the database.

### GetRoutesByDrivingDate (string: drivingDateString)
- Request: HttpGet
- Endpoint: api/Routes/getRoutesByDrivingDate/{drivingDateString}
- Result: Json(ResponseCollection(RouteDTO))
- Explanation: This method gets a Route by its driving date.

## UsersController

### GetOwn ()

- Request type: HttpGet
- Endpoint: api/Users/getOwn
- Result: Json(ResponseModel(UserGetOwnDTO))
- Explanation: This methods return the current user.

### GetData ()

- Request type: HttpGet
- Endpoint: api/Users/getData
- Result: Json(Response)
- Explanation: This methods verify if the token is valid.
  
### GetAllUsers ()

- Request type: HttpGet
- Endpoint: api/Users/getAll
- Result: Json(ResponseCollection(UserDTO))
- Explanation: This methods return a list with all users data.

### GetUserByEmail ( model: string )

- Request type: HttpGet
- Endpoint: api/Users/getUserByEmail/{email}
- Result: Json(ResponseModel(UserDTO))
- Explanation: This methods return the user data associated to the given ID.

### Register ( model: UserRegisterDTO )

- Request type: HttpPost
- Endpoint: api/Users/register
- Result: Json(Response)
- Explanation: This methods add the user to the DB.

### DeleteUser ( email: string )

- Request type: HttpPost
- Endpoint: api/Users/deleteUser/{email}
- Result: Json(Response)
- Explanation: This methods delete the user of the DB.

### AddDevice ( model: DeviceDTO )

- Request type: HttpPost
- Endpoint: api/Users/device/addDevice
- Result: Json(Response)
- Explanation: This methods add the device to the DB.

### NotifyUser ( model: UserNotificationDTO)

- Request type: HttpPost
- Endpoint: api/Users/notifications
- Result: Json(Response)
- Explanation: This methods send a push notification to the user.