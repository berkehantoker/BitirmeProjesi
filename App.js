import React, { useState, useEffect } from 'react';
import axios from 'axios';

const App = () => {
    const [questions, setQuestions] = useState([]);
    const [answers, setAnswers] = useState({});
    const [currentPage, setCurrentPage] = useState(0);
    const pageSize = 5;

    
    const likertLabels = {
        1: "Kesinlikle Katılmıyorum",
        2: "Katılmıyorum",
        3: "Kararsızım",
        4: "Katılıyorum",
        5: "Kesinlikle Katılıyorum"
    };

    useEffect(() => {
        
        axios.get('https://localhost:7171/api/questions')
            .then(res => setQuestions(res.data))
            .catch(err => console.error("Veri çekme hatası:", err));
    }, []);

    const handleOptionChange = (qId, val) => {
        setAnswers({ ...answers, [qId]: val });
    };

    const startIndex = currentPage * pageSize;
    const currentQuestions = questions.slice(startIndex, startIndex + pageSize);

    const submitTest = () => {
        const payload = {
            userId: 1,
            responses: Object.entries(answers).map(([id, val]) => ({
                questionId: parseInt(id),
                answerValue: val
            }))
        };
        alert("Test bitti! Veriler backend'e gönderilmeye hazır.");
        console.log("Gönderilecek JSON:", payload);
    };

    return (
        <div style={{ fontFamily: 'Segoe UI, Tahoma, sans-serif', maxWidth: '1000px', margin: '50px auto', padding: '30px', boxShadow: '0 0 15px rgba(0,0,0,0.1)', borderRadius: '12px', backgroundColor: '#fff' }}>
            <h2 style={{ textAlign: 'center', color: '#2c3e50', marginBottom: '30px' }}>Siber Suç Farkındalık Testi (CAS)</h2>

            {/* İlerleme Çubuğu */}
            <div style={{ background: '#eee', height: '12px', borderRadius: '10px', marginBottom: '40px', overflow: 'hidden' }}>
                <div style={{ width: `${(Object.keys(answers).length / 41) * 100}%`, background: '#27ae60', height: '100%', transition: '0.4s ease' }}></div>
            </div>

            {currentQuestions.map((q) => (
                <div key={q.id} style={{ marginBottom: '40px', padding: '25px', border: '1px solid #f0f0f0', borderRadius: '8px', background: '#fcfcfc' }}>
                    <p style={{ fontSize: '18px', color: '#34495e', marginBottom: '20px' }}><strong>{q.id}.</strong> {q.questionText}</p>

                    {/* 5'li Likert Sistemi  */}
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', gap: '10px' }}>
                        {[1, 2, 3, 4, 5].map(num => (
                            <label key={num} style={{ flex: 1, textAlign: 'center', cursor: 'pointer', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                                <input
                                    type="radio"
                                    name={`q${q.id}`}
                                    onChange={() => handleOptionChange(q.id, num)}
                                    checked={answers[q.id] === num}
                                    style={{ transform: 'scale(1.4)', marginBottom: '12px', cursor: 'pointer' }}
                                />
                                <span style={{ fontSize: '11px', color: '#7f8c8d', lineHeight: '1.2', fontWeight: answers[q.id] === num ? 'bold' : 'normal' }}>
                                    {likertLabels[num]}
                                </span>
                            </label>
                        ))}
                    </div>
                </div>
            ))}

            <div style={{ display: 'flex', justifyContent: 'center', gap: '25px', marginTop: '40px' }}>
                {currentPage > 0 && (
                    <button
                        onClick={() => setCurrentPage(currentPage - 1)}
                        style={{ padding: '12px 35px', cursor: 'pointer', borderRadius: '6px', border: '1px solid #bdc3c7', background: '#fff' }}
                    >
                        Geri
                    </button>
                )}

                {startIndex + pageSize < questions.length ? (
                    <button
                        onClick={() => setCurrentPage(currentPage + 1)}
                        disabled={currentQuestions.some(q => !answers[q.id])}
                        style={{
                            padding: '12px 35px',
                            cursor: currentQuestions.some(q => !answers[q.id]) ? 'not-allowed' : 'pointer',
                            background: currentQuestions.some(q => !answers[q.id]) ? '#bdc3c7' : '#3498db',
                            color: 'white',
                            border: 'none',
                            borderRadius: '6px',
                            transition: '0.3s'
                        }}
                    >
                        Sonraki Sayfa
                    </button>
                ) : (
                    <button
                        onClick={submitTest}
                        disabled={Object.keys(answers).length < 41}
                        style={{
                            padding: '12px 35px',
                            cursor: Object.keys(answers).length < 41 ? 'not-allowed' : 'pointer',
                            background: '#27ae60',
                            color: 'white',
                            border: 'none',
                            borderRadius: '6px',
                            fontWeight: 'bold'
                        }}
                    >
                        Testi Tamamla
                    </button>
                )}
            </div>
        </div>
    );
};

export default App;