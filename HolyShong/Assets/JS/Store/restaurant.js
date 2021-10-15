let app = new Vue({
    el: '#app',
    data: {
        product: {
            ProductId: 1,
            ProductName: '',
            ProductDescription: '',
            UnitPrice: 69.0,
            ProductImg: '',
            StoreProductOptions: [
                {
                    ProductOptionName: '',
                    ProductOptionDetails: [
                        {
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptioinDetailName: ''
                        }
                    ]
                },
                {
                    ProductOptionName: '',
                    ProductOptionDetails: [
                        {
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptioinDetailName: ''
                        },
                        {
                            StoreProductOptioinDetailName: ''
                        }
                    ]
                }
            ]
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
            url: '/Store/GetProductModal',
            contentType: 'application/json',
            data: JSON.stringify({ ProductId: cardid }),
            success: function (res) {
                console.log(res);
                app.product = JSON.parse(res);
            }
        });
    });
});