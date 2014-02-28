// Some common scripts that we use everywhere
function request(url, data, onSuccess) {
	return $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(data),
        success: onSuccess,
        contentType: 'application/json',
        dataType: "json"
    });
}
