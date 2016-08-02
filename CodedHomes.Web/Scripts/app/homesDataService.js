var httpVerbs = {
    POST: 'POST',
    PUT: 'PUT',
    GET: 'GET',
    DEL: 'DELETE'
};

var homesDataService = function () {
    var
        ds = {
            commit: function (type, url, data) {

                if (type === httpVerbs.POST)
                    delete data['id'];

                return $.ajax({
                    type: type,
                    url: url,
                    data: data,
                    dataType: 'json'
                });
            },

            del: function (data) {
                return this.commit(httpVerbs.DEL, '/api/homes/' + data.id);
            },

            save: function (data) {
                var
                    type = httpVerbs.POST,
                    url = '/api/homes';

                if (data.id > 0) {
                    type = httpVerbs.PUT;
                    url += '/' + data.id;
                }

                return this.commit(type, url, data);
            },

            saveImage: function (data) {
                return $.ajax({
                    type: httpVerbs.POST,
                    url: '/homes/uploadimage',
                    processData: false,
                    contentType: false,
                    data:data
                });
            }

        };
    _.bindAll(ds, 'del', 'save');

    return {
        save: ds.save,
        del: ds.del,
        saveImage:ds.saveImage
    }
}();