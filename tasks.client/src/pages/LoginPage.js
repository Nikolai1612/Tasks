import React from 'react';
import { Formik, Form, Field } from 'formik';
import * as Yup from 'yup';

const LoginSchema = Yup.object().shape({
  email: Yup.string().email('Invalid email').required('Required'),
  password: Yup.string().min(6, 'Too Short!').required('Required'),
});

function LoginPage() {
  const handleSubmit = (values) => {
    // Взаимодействие с API для аутентификации
    console.log(values);
  };

  return (
    <div>
      <h1>Login</h1>
      <Formik
        initialValues={{ email: '', password: '' }}
        validationSchema={LoginSchema}
        onSubmit={handleSubmit}
      >
        {({ errors, touched }) => (
          <Form>
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
            <button type="submit">Login</button>
          </Form>
        )}
      </Formik>
    </div>
  );
}

export default LoginPage;