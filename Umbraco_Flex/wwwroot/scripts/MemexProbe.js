function SetPInnerText(data) {
	let div = document.getElementById("MemexData");
	console.log(data);
	for (var i = 0; i < data.length; i++) {
		h = document.createElement("h6");
		h.innerText = data[i].name;
		p = document.createElement("p");
		p.innerText = data[i].description;
		div.appendChild(h);
		div.appendChild(p);
	}
}

function ProbeForData() {
		$.ajax({
			type: "GET",
			url: "/umbraco/api/MemexPortalApi/GetProducts",
			success: function (data) {
				SetPInnerText(data);
			}
		})
}