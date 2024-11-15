import React from 'react'
import Header from '../components/Header'
import ChatComponent from '../components/User/chatComponent'
import Footer from '../components/Footer'

function Chat() {
  return (
    <div>
        <Header/>
      <ChatComponent/>
      <Footer/>
    </div>
  )
}

export default Chat