let app = new Vue({
    el: '#app',
    data: {
        select: '',
        product: {
            ProductId: 1,
            ProductName: '',
            ProductDescription: '',
            UnitPrice: 69.0,
            ProductImg: '',
            Quantity: 1,
            StoreProductOptions: [
                {
                    ProductOptionName: '',
                    ProductOptionDetails: [
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        }
                    ]
                },
                {
                    ProductOptionName: '',
                    ProductOptionDetails: [
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptionDetailId: '',
                            StoreProductOptioinDetailName: ''
                        }
                    ]
                }
            ]
        }
    },
    methods: {
        changeAmount(value) {
            if (this.product.Quantity + value == 0) { return; }
            this.product.Quantity += value;
        },
    },
    computed: {
        sum() {
            return this.product.UnitPrice * this.product.Quantity;
        }
    }
});


//愛心
let heartSolid = document.querySelector(".heartSolid");
let heartEmpty = document.querySelector(".heartEmpty");
//let heart = document.querySelector(".heart")

//heart.onclick = function () {
//    if ($('.heartEmpty').css('display') === "block") {
//        heartEmpty.style.display = "none";
//        heartSolid.style.display = "block";
//    } else {
//        heartEmpty.style.display = "block";
//        heartSolid.style.display = "none";
//    }
//}

//按卡片取product內容
let productCards = document.querySelectorAll('.cardProduct');
productCards.forEach((card, index) => {
    card.addEventListener('click', function (event) {
        let cardid = card.getAttributeNode('data-id').value;

        console.log(cardid);
        $.ajax({
            type: 'POST',
            url: '/api/Store/GetProductModal',
            contentType: 'application/json',
            data: JSON.stringify({ ProductId: cardid }),
            success: function (res) {
                console.log(res);
                app.product = res;
                $('#cardModal').modal('show')
            }
        });
    });
});

