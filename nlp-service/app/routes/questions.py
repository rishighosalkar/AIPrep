from fastapi import APIRouter
from pydantic import BaseModel
from typing import List
import random

router = APIRouter()

#Request model
class QuestionRequest(BaseModel):
    role: str
    
#Response model
class Question(BaseModel):
    text: str
    source: str="AI"
    
#Dummy question generation logic
@router.post("/generate", response_model=List[Question])
def generate_questions(req: QuestionRequest) :
    role = req.role.lower()
    question_bank = {
        "backend developer": [
            "What is the difference between monolithic and microservices architecture?",
            "How does garbage collection work in .NET?",
            "What are the SOLID principles?"
        ],
        "frontend developer": [
            "Explain the virtual DOM in React.",
            "What is the difference between controlled and uncontrolled components?",
            "Describe the lifecycle methods in React."
        ],
        "data scientist": [
            "What is the difference between supervised and unsupervised learning?",
            "How does a random forest algorithm work?",
            "Explain precision, recall, and F1 score."
        ]
    }
    
    selected_questions = question_bank.get(role, ["Tell me about yourself.", "Why do you want this role?"])
    #Return 3 randomly selected questions
    generated = random.sample(selected_questions, min(3, len(selected_questions)))
    return [{"text": q} for q in generated]