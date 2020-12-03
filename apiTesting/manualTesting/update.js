var axios = require('axios');
var data = JSON.stringify({
  "id": "5fc8a5878244ce7af97d65bc",
  "email": "rando.user@hotmail.com",
  "firstName": "random",
  "lastName": "Guy",
  "title": "MR",
  "dateOfBirth": "1983-06-11T00:00:00",
  "phoneNumber": "022 234 5678",
  "imageUrl": "image.png",
  "thumbnailUrl": "thumb/nail.png"
});

var config = {
  method: 'put',
  url: 'http://localhost:5001/user',
  headers: { 
    'Content-Type': 'application/json'
  },
  data : data
};

axios(config)
.then(function (response) {
  console.log(JSON.stringify(response.data, null, 2));
})
.catch(function (error) {
  console.log(error);
});
