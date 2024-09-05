import React from 'react';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';

const RegisterSchema = Yup.object().shape({
  username: Yup.string().required('Required'),
  email: Yup.string().email('Invalid email').required('Required'),
  password: Yup.string().min(6, 'Too Short!').required('Required'),
});

function RegisterPage() {
  const handleSubmit = (values) => {
    // Взаимодействие с API для регистрации
    console.log(values);
  };

  return (
    <div>
      <h1>Register</h1>
      <Formik
        initialValues={{ username: '', email: '', password: '' }}
        validationSchema={RegisterSchema}
        onSubmit={handleSubmit}
      >
        {({ errors, touched }) => (
          <Form>
            <div>
              <label>Username</label>
              <Field name="username" type="text" />
              {errors.username && touched.username ? <div>{errors.username}</div> : null}
            </div>
            <div>
              <label>Email</label>
              <Field name="email" type="email" />
              {errors.email && touched.email ? <div>{errors.email}</div> : null}
            </div>
            <div>
              <label>Password</label>
              <Field name="password" type="password" />
              {errors.password && touched.password ? <div>{errors.password}</div> : null}
            </div>
            <button type="submit">Register</button>
          </Form>
        )}
      </Formik>
    </div>
  );
}

export default RegisterPage;