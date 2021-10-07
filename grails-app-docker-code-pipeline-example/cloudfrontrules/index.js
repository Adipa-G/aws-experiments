'use strict';
const querystring = require('querystring');

exports.handler = (event, context, callback) => {
  const request = event.Records[0].cf.request;

  const headers = request.headers;
  const params = querystring.parse(request.querystring);

  if (params.x){
	const redirectUrl = 'http://' + headers.host[0].value + request.uri + params.x;
	const response = {
		status: '302',
		statusDescription: 'Found',
		headers: {
			location: [{
				key: 'Location',
				value: redirectUrl,
			}],
		},
	};
	callback(null, response);
  }
  else{
	callback(null, request);
  }
};