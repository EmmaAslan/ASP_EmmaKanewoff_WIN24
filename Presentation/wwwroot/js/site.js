document.addEventListener('DOMContentLoaded', () => {

    // open modal
    const modalButtons = document.querySelectorAll('[data-modal="true"]')
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)

            if (modal)
                modal.style.display = "flex";
        })
    })


    // close modal
    const closeButtons = document.querySelectorAll('[data-close="true"]')
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal')

            if (modal) {
                modal.style.display = 'none'

                //clear formdata


            }
        })
    })
})

// 1:12:58 - https://www.youtube.com/watch?v=mTxeMh5SPJo