var axios = require('axios');
var data = JSON.stringify(
  {
    "firstName":"random",
    "email":"rando.user@gmail.com",
    "lastName":"Guy",
    "title":"MR",
    "phoneNumber":"022 234 5678",
    "dateOfBirth":"1983-06-11",
    "thumbnailUrl":"thumb/nail.png",
    "imageUrl":"image.png"
  });

var config = {
  method: 'post',
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
