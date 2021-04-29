import React, { Fragment } from 'react'
import { Form } from 'formik';
import { withFormik } from 'formik';
import { Typography, Button } from '@material-ui/core';
import { DialogCloseIcon } from "../Buttons/IconButtons"
import { FormTextField, FormSelectField } from "./FormFields"

export const SubscriptionForm = ({
    values,
    handleChange,
    handleSubmit,
    handleClose,
    membershipOptions,
    setFieldValue
}) => {
    return <Fragment>
        <DialogCloseIcon handleClose={handleClose} />
        <div align="center"><Typography variant="h5">Membership</Typography></div>
        <Form onSubmit={handleSubmit}>
            <div align="center" className="form">
                <FormTextField id="startDate" inputLabel="Membership Start Date *" value={values.startDate} handleChange={handleChange} />
                <FormSelectField id="membershipid" inputLabel="Membership *" options={membershipOptions}
                    handleChange={(e) => setFieldValue("membershipid", e.value)} />
                <br />
                <br />
                <Button
                    className="navbar-create-btn"
                    onClick={handleSubmit}
                >Subscribey</Button>
            </div>
        </Form>
    </Fragment>
}

const SubscriptionFormWithFormik = withFormik({

    mapPropsToValues: ({ startDate = "", membershipid = 0 }) => ({
        startDate: startDate,
        membershipid: membershipid,
    }),

    // Custom sync validation
    validate: values => {
        const errors = {}
        if (!values.startDate || values.startDate === "") {
            errors.startDate = 'Required';
        }
        if (!values.membershipid || values.membershipid === 0) {
            errors.membershipid = 'Required';
        }
        return errors;
    },
    handleSubmit: (values, { 'props': { onContinue } }) => {
        onContinue({
            startDate: values.startDate,
            membershipid: values.membershipid,
        });
    },
    displayName: 'Subscription Form',
})(SubscriptionForm);

export default SubscriptionFormWithFormik