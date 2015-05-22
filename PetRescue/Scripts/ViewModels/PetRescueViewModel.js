
function PetRescueViewModel() {
    var self = this;
    self.pets = ko.observableArray();
    self.petDetails = ko.observable();
    self.owners = ko.observableArray();
    self.search = ko.observable('');
    self.foundFilters = ['All', 'Lost', 'Found'];
    self.foundFilter = ko.observable('All');
    self.isFoundFilter = ko.observable();
    self.newPet = {
        Name: ko.observable(),
        ImagePath: ko.observable(),
        Type: ko.observable(),
        Description: ko.observable(),
        Found: ko.observable(),
        Owner: ko.observable()
    }
    self.editPet = {
        Id: ko.observable(),
        Name: ko.observable(),
        Type: ko.observable(),
        Description: ko.observable(),
        Found: ko.observable()
    }
    self.error = ko.observable();
    //self.navItems = ['Pets', 'Owners'];

    // API URI's
    var petsUri = 'api/pets/';
    var ownersUri = 'api/owners/';

    //ajax helper
    function ajaxHelper(uri, method, data) {
        self.error(''); //clear error
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    };

    //get all pets
    function getAllPets() {
        ajaxHelper(petsUri, 'GET').done(function (data) {
            self.pets(data);
        });
    }
    //get all Owners
    function getAllOwners() {
        ajaxHelper(ownersUri, 'GET').done(function (data) {
            self.owners(data);
        });
    }

    //get the details of a pet
    self.getPetDetails = function (item) {
        //get pet details
        ajaxHelper(petsUri + item.Id, 'GET').done(function (data) {
            self.petDetails(data);
        });
    }

    //add a new pet
    self.addPet = function (formElement) {

        //upload image and return the url
        var imgData = new FormData();
        imgData.append("petImg", self.newPet.ImagePath());

        $.ajax({
            type: "POST",
            url: "/api/ImageUpload",
            contentType: false,
            processData: false,
            data: imgData,
            success: function (serverPicURL) {
                //upload new pet
                var pet = {
                    Name: self.newPet.Name(),
                    PicURL: "",
                    Type: self.newPet.Type(),
                    Description: self.newPet.Description(),
                    Found: self.newPet.Found(),
                    OwnerId: self.newPet.Owner().Id
                };

                ajaxHelper(petsUri, 'POST', pet).done(function (item) {
                    self.pets.push(item);
                    //clear newPet
                    self.newPet.Name("");
                    self.newPet.Type("");
                    self.newPet.Description("");
                    alert("New Pet Added Successfully");
                });
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    };

    //setup and start editing of a pet
    self.startPetEdit = function () {
        self.editPet.Id(self.petDetails().Id);
        self.editPet.Name(self.petDetails().Name);
        self.editPet.Type(self.petDetails().Type);
        self.editPet.Description(self.petDetails().Description);
        self.editPet.Found(self.petDetails().Found);
    }
    //end pet edit
    self.endPetEdit = function () {
        self.editPet.Name(null);
    }

    //update a pet
    self.updatePet = function (formElement) {
        var pet = {
            Id: self.editPet.Id(),
            Name: self.editPet.Name(),
            Type: self.editPet.Type(),
            Description: self.editPet.Description(),
            Found: self.editPet.Found(),
            OwnerId: self.petDetails().OwnerId
        };
        ajaxHelper(petsUri + pet.Id, 'PUT', pet).done(function (item) {
            //refresh pets
            getAllPets();
            //set new pet details
            self.petDetails(item);
            //end edit
            self.endPetEdit();
            alert("Pet Updated Successfully");
        });
    }

    //delete a pet
    self.deletePet = function (item) {
        //confirm delete request
        var proceed = confirm("Are you sure you want to delete "+item.Name+" the "+item.Type+"?");
        if (proceed == true) {
            ajaxHelper(petsUri + item.Id, 'DELETE').done(function (data) {
                //refresh pets
                getAllPets();
                //hide pet details (incase deleted record is shown)
                self.petDetails(null);
                alert("Pet with name " + item.Name + " deleted successfully");
            });
        }
    }

    //set found filter
    self.setFoundFilter = function (filter) {
        self.foundFilter(filter);
        switch(filter){
            case 'All': self.isFoundFilter(null); break;
            case 'Lost': self.isFoundFilter(false); break;
            case 'Found': self.isFoundFilter(true); break;
            default: self.isFoundFilter(null);
        }
    };

    //filter pets (search)
    self.filteredPets = ko.computed(function () {
        //use array filter utility
        return ko.utils.arrayFilter(self.pets(), function (record) {
            //allow record if (search field is empty OR record field contains search request) AND (found filter is null [All] OR record found filtered [found/lost])
            return (self.search().length == 0 || record.Name.toLowerCase().indexOf(self.search().toLowerCase()) >= 0 || record.Type.toLowerCase().indexOf(self.search().toLowerCase()) >= 0) && (self.isFoundFilter() == null || record.Found == self.isFoundFilter())
        });
    });

    //fetch initial data
    getAllPets();
    getAllOwners();
};

ko.applyBindings(new PetRescueViewModel());