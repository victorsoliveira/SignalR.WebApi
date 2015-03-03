var TodoViewModel = function(id, description, connId) {
    return {
        id: id,
        description: description,
        owner: connId
    }
};