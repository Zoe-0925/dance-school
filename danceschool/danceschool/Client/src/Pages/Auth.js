import React from 'react'
import LoginForm from '../Components/Forms/Login'
import { fetchGoogleLogin } from '../Components/API/Auth'
import SignUpForm from '../Components/Forms/SignUp'
import { useAuth } from '../Components/Hooks/useAuth'
import { Row, Col } from 'reactstrap'

const Auth = () => {
  const { page, setPage, login, signUp } = useAuth()

  return (
    <Row className='landing-page'>
      <Col className='landing-img' xs={12} sm={8}>
       
      </Col>
      <Col className='auth-form' xs={12} sm={4}>
        {page === 'login' && (
          <LoginForm
            onContinue={login}
            goToSignUp={() => setPage('signUp')}
            onGoogleLogin={fetchGoogleLogin}
          />
        )}
        {page === 'signUp' && (
          <SignUpForm onContinue={signUp} goToLogin={() => setPage('login')} />
        )}
      </Col>
    </Row>
  )
}

export default Auth
