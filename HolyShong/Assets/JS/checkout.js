//leafLet
let map = L.map('map', {
    center: [25.0416806, 121.5352095],
    zoom: 9
})
var osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png'
var osm = new L.TileLayer(osmUrl, { minZoom: 8, maxZoom: 9 })
map.addLayer(osm);

let checkOut = new Vue({

    el: '#checkApp',
    data: {
        store: checkInfo,

    },
    computed: {
        totalSum() {
            let totalPrice = 0;
            for (var i = 0; i < this.store.CartItems.length; i++) {
                let addAmount = 0;
                if (this.store.CartItems[i].StoreProductOptions != null) {
                    for (var j = 0; j < this.store.CartItems[i].StoreProductOptions.length; j++) {
                        for (var k = 0; k < this.store.CartItems[i].StoreProductOptions[j].ProductOptionDetails.length; k++) {
                            if (this.store.CartItems[i].StoreProductOptions[j].SelectOption == this.store.CartItems[i].StoreProductOptions[j].ProductOptionDetails[k].StoreProductOptionDetailId) {
                                addAmount += this.store.CartItems[i].StoreProductOptions[j].ProductOptionDetails[k].AddPrice;
                            }
                        }
                    }
                }
                totalPrice += this.store.CartItems[i].Quantity * (this.store.CartItems[i].UnitPrice + addAmount)
            }
            return totalPrice;
        }
    },
    methods: {
        sum(p) {
            console.log(p)
            let addAmount = 0;
            if (p.StoreProductOptions == null) { return p.UnitPrice * p.Quantity; }
            for (var i = 0; i < p.StoreProductOptions.length; i++) {
                for (var j = 0; j < p.StoreProductOptions[i].ProductOptionDetails.length; j++) {
                    if (p.StoreProductOptions[i].ProductOptionDetails[j].StoreProductOptionDetailId == p.StoreProductOptions[i].SelectOption) {
                        addAmount += p.StoreProductOptions[i].ProductOptionDetails[j].AddPrice;
                    }
                }
            }
            return (p.UnitPrice + addAmount) * p.Quantity;
        },
        sendForm() {
            let btn = document.querySelector('#sendFormToOrder');
            btn.click();
        }
    }
});

//export default {
//    data() {
//        return {
//            selected: null,
//            options: [
//                { value: null, text: 'Please select an option' },
//                { value: 'a', text: 'This is First option' },
//                { value: 'b', text: 'Selected Option' },
//                { value: { C: '3PO' }, text: 'This is an option with object value' },
//                { value: 'd', text: 'This one is disabled', disabled: true }
//            ]
//        }
//    }
//}