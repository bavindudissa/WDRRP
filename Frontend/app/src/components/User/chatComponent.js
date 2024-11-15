import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios';
import './ChatComponent.css';

function ChatComponent() {
    const [messages, setMessages] = useState([]);
    const [input, setInput] = useState('');
    const messagesEndRef = useRef(null);

    useEffect(() => {
        messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
    }, [messages]);

    const sendMessage = async () => {
        if (!input.trim()) return;

        const userMessage = { text: input, sender: 'user' };
        setMessages(prev => [...prev, userMessage]);

        try {
            const response = await axios.post('http://localhost:5093/api/chatbot/message', { message: input });
            const botMessage = { text: response.data.response, sender: 'bot' };
            setMessages(prev => [...prev, botMessage]);
        } catch (error) {
            const errorMessage = { text: 'Sorry, there was an error. Please try again later.', sender: 'bot' };
            setMessages(prev => [...prev, errorMessage]);
        }

        setInput('');
    };

    const handleKeyPress = (e) => {
        if (e.key === 'Enter') {
            sendMessage();
        }
    };

    return (
        <div>
            <div className="row fullscreen d-flex align-items-center justify-content-center" style={{ height: '70px', backgroundColor: 'black', color: 'black' }}>			
            </div>
            <br/>
            <div className="container">
                <div className="main-body">
                    <div className="chat-container">
                        <div className="chat-header">ChatBot</div>
                        <div className="chat-body">
                            {messages.map((msg, index) => (
                                <div key={index} className={`message ${msg.sender === 'user' ? 'user-message' : 'bot-message'}`}>
                                    {msg.text}
                                </div>
                            ))}
                            <div ref={messagesEndRef} />
                        </div>
                        <div className="chat-input-area">
                            <input
                                type="text"
                                placeholder="Type your message..."
                                value={input}
                                onChange={(e) => setInput(e.target.value)}
                                onKeyPress={handleKeyPress}
                                className="chat-input"
                            />
                            <button onClick={sendMessage} className="send-button">Send</button>
                        </div>
                    </div>
                    <br/>
                </div>    
            </div>

        </div>
       

    );
}

export default ChatComponent;
