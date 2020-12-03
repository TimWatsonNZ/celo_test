var axios = require('axios');

var config = {
  method: 'get',
  url: 'http://localhost:5001/user',
  headers: { }
};

axios(config)
.then(function (response) {
  console.log(JSON.stringify(response.data, null, 2));
})
.catch(function (error) {
  console.log(error);
});
