var axios = require('axios');
var config = {
  method: 'delete',
  url: 'http://localhost:5001/user/5fc8a5878244ce7af97d65bc',
  headers: { 
    'Content-Type': 'application/json'
  }
};

axios(config)
.then(function (response) {
  console.log(JSON.stringify(response.data));
})
.catch(function (error) {
  console.log(error);
});
