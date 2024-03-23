import axios from 'axios';

const apiUrl = "https://localhost:7241" 


export default {
  getTasks: async () => {
    try{
       const result = await axios.get(`${apiUrl}/items`)    
       return result.data;
    }catch(error){
      console.error('Error in getTasks:', error);//throw error;;
    }
   
  },

  addTask: async(name)=>{
    console.log('addTask', name)
    await axios.post(`${apiUrl}/items`,{name: name})  
    return {};
  },

  setCompleted: async(id, isComplete)=>{
    console.log('setCompleted', {id, isComplete})
    const result = await axios.put(`${apiUrl}/items/${id}`, {isComplete: isComplete},{ headers: { 'Content-Type': 'application/json' }});
    if(result.status === 200) {
      return {success: true}
    }else {
      return {success: false, errorMessage: "Faild to update item."};
    }
  },

  deleteTask:async(id)=>{
    console.log('deleteTask',id)
    await axios.delete(`${apiUrl}/items/${id}`) 
      .catch(error => {
        console.log('Error in deleteTask:',error);
        throw error;// אם תרצי להמשיך לטפל בשגיאה במקום אחר, תוכלי לטפל בה כאן
      })
  }

};
