// Javascript for AddProject - Fått hjälp av ChatGPT 4.o
document.addEventListener('DOMContentLoaded', function () {
    handleWysiwyg('#addProject-wysiwyg-editor', '#addProject-wysiwyg-toolbar', '#Description', "")


    const form = document.querySelector('#addProjectModal form')
    form.addEventListener('submit', async function (e) {
        e.preventDefault();

        const quill = Quill.find(document.querySelector('#addProject-wysiwyg-editor'));
        document.querySelector('#Description').value = quill.root.innerHTML;

        clearErrorMessages(form);

        const formData = new FormData(form);

        try {
            const response = await fetch(form.action, {
                method: 'POST',
                body: formData
            });


            if (response.ok) {
                const modal = form.closest('.modal');
                if (modal) {
                    modal.style.display = 'none';
                }
                window.location.reload();
            }
            else if (response.status === 400) {
                const data = await response.json();

                if (data.errors) {
                    Object.keys(data.errors).forEach(key => {
                        let fieldName = key.charAt(0).toLowerCase() + key.slice(1);
                        let input = form.querySelector(`[name="${fieldName}"]`);
                        if (input) {
                            input.classList.add('input-validation-error');
                        }

                        let span = form.querySelector(`span[data-valmsg-for="${key}"]`) ||
                            form.querySelector(`span[asp-validation-for="${key}"]`);
                        if (span) {
                            span.textContent = data.errors[key].join('\n');
                            span.classList.add('field-validation-error');
                        }
                    });
                }
            }
        } catch (error) {
            console.error('Error submitting form:', error);
            alert('There was a problem submitting the form. Please try again.');
        }
    })
})


//Fått hjälp av ChatGPT 4.o
function clearErrorMessages(form) {
    form.querySelectorAll('.input-validation-error').forEach(input => {
        input.classList.remove('input-validation-error');
    });

    form.querySelectorAll('.field-validation-error').forEach(span => {
        span.textContent = '';
        span.classList.remove('field-validation-error');
    });
}




// Javascript for EditProject 
document.addEventListener('DOMContentLoaded', function () {
    handleWysiwyg('#editProject-wysiwyg-editor', '#editProject-wysiwyg-toolbar', '#EditDescription', "")


    const form = document.querySelector('#editProjectModal form')
    form.addEventListener('submit', async function (e) {
        e.preventDefault();

        const quill = Quill.find(document.querySelector('#editProject-wysiwyg-editor'));
        document.querySelector('#EditDescription').value = quill.root.innerHTML;

        clearErrorMessages(form);

        const formData = new FormData(form);

        try {
            const response = await fetch(form.action, {
                method: 'POST',
                body: formData
            });


            if (response.ok) {
                const modal = form.closest('.modal');
                if (modal) {
                    modal.style.display = 'none';
                }
                window.location.reload();
            }
            else if (response.status === 400) {
                const data = await response.json();

                if (data.errors) {
                    Object.keys(data.errors).forEach(key => {
                        let fieldName = key.charAt(0).toLowerCase() + key.slice(1);
                        let input = form.querySelector(`[name="${fieldName}"]`);
                        if (input) {
                            input.classList.add('input-validation-error');
                        }

                        let span = form.querySelector(`span[data-valmsg-for="${key}"]`) ||
                            form.querySelector(`span[asp-validation-for="${key}"]`);
                        if (span) {
                            span.textContent = data.errors[key].join('\n');
                            span.classList.add('field-validation-error');
                        }
                    });
                }
            }
        } catch (error) {
            console.error('Error submitting form:', error);
            alert('There was a problem submitting the form. Please try again.');
        }
    })
})



document.addEventListener('DOMContentLoaded', function () {
    const editProjectBtn = document.querySelectorAll('#editProject-btn');

    editProjectBtn.forEach((btn) => {
        btn.addEventListener('click', async function () {
            const projectId = this.getAttribute('data-projectId');
            if (projectId) {
                try {
                    const response = await fetch(`getprojects/${projectId}`);

                    if (response.ok) {
                        const memberdata = await response.json();

                        handleWysiwyg('#editProject-wysiwyg-editor', '#editProject-wysiwyg-toolbar', '#EditDescription', memberdata.description);

                        const form = document.querySelector('#editProjectModal form');

                        form.querySelector('[name="Id"]').value = memberdata.id;
                        form.querySelector('[name="ProjectName"]').value = memberdata.projectName;
                        form.querySelector('[name="ClientName"]').value = memberdata.clientName;
                        form.querySelector('[name="Description"]').value = memberdata.description;
                        form.querySelector('[name="StartDate"]').value = memberdata.startDate;
                        form.querySelector('[name="EndDate"]').value = memberdata.endDate;
                        form.querySelector('[name="Budget"]').value = memberdata.budget;
                        form.querySelector('[name="StatusId"]').value = memberdata.statusId;
                    }
                } catch (error) {
                    console.error('Error fetching project data:', error);
                }
            }

        })
    })
})


function handleWysiwyg(wysiwygEditorId, wysiwygToolbarId, textareaId, content) {
    const textarea = document.querySelector(textareaId)
    const quill = new Quill(wysiwygEditorId, {
        modules: {
            syntax: true,
            toolbar: wysiwygToolbarId,
        },
        placeholder: 'Enter project description',
        theme: 'snow'
    })

    if (content) {
        quill.root.innerHTML = content;
    }

    quill.on('text-change', () => {
        textarea.value = quill.root.innerHTML;
    })
}