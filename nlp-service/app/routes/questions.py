from pathlib import Path
from fastapi import APIRouter, HTTPException
from pydantic import BaseModel
from typing import List
import random
import yaml

router = APIRouter()

#Request model
class QuestionRequest(BaseModel):
    role: str
    
#Response model
class Question(BaseModel):
    id: str
    role: str
    question_text: str
    ideal_answer: str
    source: str="AI"

# question_pool_by_role = {}

def load_static_questions():
    question_pool_by_role = {}
    path = Path(__file__).parent.parent.parent / "questions.yaml"
    with open(path, "r") as f:
        data = yaml.safe_load(f);
        question_pool_by_role = data["questions_by_role"]
    return question_pool_by_role
        
#Dummy question generation logic
@router.post("/questions", response_model=List[Question])
def generate_questions(req: QuestionRequest) :
    role = req.role.lower()
    question_pool_by_role = load_static_questions()
    questions = question_pool_by_role.get(role)
    if not questions:
        raise HTTPException(status_code=404, detail="Role not found")
    #Return 3 randomly selected questions
    generated = random.sample(questions, min(3, len(questions)))    
    return [
        Question(
            id = q["id"],
            role = role,
            question_text=q["text"],
            ideal_answer=q["ideal_answer"],
            ) for q in generated]