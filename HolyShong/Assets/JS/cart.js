let plus = document.querySelector('#plus')
let minus = document.querySelector('#minus')
let quantity = document.querySelector('#quantity')




plus.addEventListener('click', function () {
    let quantityValue = document.querySelector('#quantity').innerText
    quantity.innerText = ++quantityValue

})

minus.addEventListener('click', function () {
    let quantityValue = document.querySelector('#quantity').innerText
    if (quantityValue > 1) {
        quantity.innerText = --quantityValue
    }
})  